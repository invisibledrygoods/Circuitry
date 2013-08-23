using UnityEngine;
using System.Collections.Generic;
using Require;
using System;
using System.Reflection;

public class CircuitComponent : MonoBehaviour
{
    public void Chain(string name, CircuitComponent to)
    {
        foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
            if (field.Name.ToLower() == name.ToLower())
            {
                List<CircuitComponent> components = field.GetValue(this) as List<CircuitComponent>;
                Debug.Log(components);
                if (components != null)
                {
                    Debug.Log("adding");
                    components.Add(to);
                    return;
                }
            }
        }

        throw new KeyNotFoundException("No public field of type List<CircuitComponent> exists by name: " + name);
    }

    public void Spark(string name)
    {
        foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
            if (field.Name.ToLower() == name.ToLower())
            {
                List<CircuitComponent> components = field.GetValue(this) as List<CircuitComponent>;
                if (components != null)
                {
                    foreach (CircuitComponent component in components)
                    {
                        component.enabled = true;
                    }
                }
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

                    turtle.PenDown();
                    turtle.Forward(0.2f);
                    turtle.RotateLeft(90);
                    turtle.Forward(0.05f);
                    turtle.RotateRight(120);
                    turtle.Forward(0.1f);
                    turtle.RotateRight(150);
                    turtle.Forward(0.1f);
                    turtle.RotateRight(180);

                    turtle.PenUp();
                    turtle.Forward(0.15f);

                    if (field.Name.ToLower() != "next")
                    {
                        font.Write(field.Name);
                    }

                    turtle.Forward(0.05f);

                    turtle.PenDown();
                    turtle.Forward(Vector3.Distance(turtle.Position, component.transform.position));
                }
            }
        }

        Gizmos.color = oldColor;
    }
}
