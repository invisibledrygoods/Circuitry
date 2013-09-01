using UnityEngine;
using System.Collections.Generic;
using Require;
using System;
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
        Color oldColor = Gizmos.color;

        if (enabled)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
        else
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }

        Gizmos.color = Color.white;

        foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
            List<CircuitComponent> components = field.GetValue(this) as List<CircuitComponent>;
            if (components != null)
            {
                foreach (CircuitComponent component in components)
                {
                    GizmoTurtle turtle = new GizmoTurtle(new Ray(transform.position, component.transform.position - transform.position));
                    RobotLetters font = new RobotLetters(turtle, 0.2f);

                    turtle.PenDown().Forward(0.2f).RotateLeft(90).Forward(0.05f).RotateRight(120).Forward(0.1f).RotateRight(150).Forward(0.1f).RotateRight(180);
                    turtle.PenUp().Forward(0.15f);

                    if (field.Name.ToLower() != "next")
                    {
                        font.Write(field.Name);
                    }

                    turtle.PenUp().Forward(0.05f);
                    turtle.PenDown().Forward(Vector3.Distance(turtle.Position, component.transform.position));
                }
            }
        }

        Gizmos.color = oldColor;
    }
}
