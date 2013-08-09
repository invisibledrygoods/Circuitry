using UnityEngine;
using System.Collections.Generic;
using Require;
using System;

[Serializable]
public class Edge
{
    public string name;
    public List<CircuitComponent> to;

    public Edge(string name)
    {
        this.name = name;
        to = new List<CircuitComponent>();
    }

    public void To(CircuitComponent component)
    {
        this.to.Add(component);
    }

    public void Spark()
    {
        foreach (CircuitComponent component in to)
        {
            component.enabled = true;
        }
    }
}

public class ComplexCircuitComponent : CircuitComponent
{
    public List<Edge> edges;

    public Edge Chain(string name)
    {
        edges = edges ?? new List<Edge>();

        foreach (Edge edge in edges)
        {
            if (edge.name == name)
            {
                return edge;
            }
        }
        Edge newEdge = new Edge(name);
        edges.Add(newEdge);
        return newEdge;
    }

    public override void Spark()
    {
        Spark("next");
    }

    public override void Spark(string name)
    {
        Chain(name).Spark();
        enabled = false;
    }

    public override IEnumerable<Vector3> GetWireEndpoints()
    {
        foreach (Edge edge in edges)
        {
            foreach (CircuitComponent component in edge.to)
            {
                yield return component.transform.position;
            }
        }
    }
}
