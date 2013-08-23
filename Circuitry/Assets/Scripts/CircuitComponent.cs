using UnityEngine;
using System.Collections.Generic;
using Require;
using System;
using System.Reflection;

public class CircuitComponent : MonoBehaviour
{
    public void Spark(List<CircuitComponent> edge)
    {
        if (edge != null)
        {
            foreach (CircuitComponent component in edge)
            {
                component.enabled = true;
            }
        }

        enabled = false;
    }

    public void DrawWires()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = Color.yellow;

        if (enabled)
        {
            Gizmos.DrawWireSphere(transform.position, 0.4f);
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
                    RobotLetters font = new RobotLetters(turtle, 0.1f);

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
