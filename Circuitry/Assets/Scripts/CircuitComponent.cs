using UnityEngine;
using System.Collections.Generic;
using Require;
using System;
using System.Text.RegularExpressions;
using System.Reflection;

public abstract class CircuitComponent : MonoBehaviour
{
    public static int stackLimit = 100;

    float lastTimestamp;
    int numberOfSparksThisTimestamp;

    public void Spark(List<CircuitComponent> edge)
    {
        if (lastTimestamp != Time.time)
        {
            lastTimestamp = Time.time;
            numberOfSparksThisTimestamp = 0;
        }

        numberOfSparksThisTimestamp++;
        if (numberOfSparksThisTimestamp > stackLimit)
        {
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (field.GetValue(this) == edge)
                {
                    throw new StackOverflowException("Short circuit in " + name + " along '" + field.Name + "' edge");
                }
            }
            throw new StackOverflowException("Short circuit in " + name + " along unknown edge");
        }

        enabled = false;

        if (edge != null)
        {
            foreach (CircuitComponent component in edge)
            {
                component.enabled = true;
            }
        }
    }

    public void DrawWires()
    {
        if (enabled)
        {
            GizmoTurtle.WithGizmoColor(Color.yellow, () =>
            {
                Gizmos.DrawWireSphere(transform.position, 0.1f);
            });
        }
        else
        {
            GizmoTurtle.WithGizmoColor(Color.gray, () =>
            {
                Gizmos.DrawWireSphere(transform.position, 0.1f);
            });
        }

        GizmoTurtle.WithGizmoColor(Color.white, () =>
        {
            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                List<CircuitComponent> components = field.GetValue(this) as List<CircuitComponent>;
                if (components != null)
                {
                    foreach (CircuitComponent component in components)
                    {
                        if (field.Name.ToLower() != "next")
                        {
                            DrawWire(Regex.Replace(field.Name, @"[A-Z]", " $0").Trim(), component.transform.position);
                        }
                        else
                        {
                            DrawWire("", component.transform.position);
                        }
                    }
                }

                if (this is CircuitDictionaryComponent)
                {
                    foreach (CircuitDictionaryItem item in (this as CircuitDictionaryComponent).transitions)
                    {
                        foreach (CircuitComponent component in item.transition)
                        {
                            if (item.transitionName.ToLower() != "next")
                            {
                                DrawWire(Regex.Replace(item.transitionName, @"[A-Z]", " $0").Trim(), component.transform.position);
                            }
                            else
                            {
                                DrawWire("", component.transform.position);
                            }
                        }
                    }
                }
            }
        });
    }

    void DrawWire(string label, Vector3 to)
    {
        GizmoTurtle turtle = new GizmoTurtle(new Ray(transform.position, to - transform.position));
        RobotLetters font = new RobotLetters(turtle, 0.1f);

        turtle.PenDown().Forward(0.2f).RotateLeft(90).Forward(0.05f).RotateRight(120).Forward(0.1f).RotateRight(150).Forward(0.1f).RotateRight(180);
        turtle.PenUp().Forward(0.15f);

        font.Write(Regex.Replace(label, @"[A-Z]", " $0").Trim());

        turtle.PenUp().Forward(0.05f);
        turtle.PenDown().Forward(Vector3.Distance(turtle.Position, to));
    }

    public void DrawLabel()
    {
        GizmoTurtle.WithGizmoColor(Color.white, () =>
        {
            GizmoTurtle turtle = new GizmoTurtle(transform.position);
            RobotLetters font = new RobotLetters(turtle, 0.1f);

            turtle.Forward(0.15f);
            turtle.RotateRight(90);
            turtle.Forward(0.02f);
            turtle.RotateLeft(90);
            font.Write(Regex.Replace(name, @"[A-Z]", " $0").Trim());
        });
    }
}
