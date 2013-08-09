using UnityEngine;
using System.Collections.Generic;
using Require;

public class SimpleCircuitComponent : CircuitComponent
{
    public List<CircuitComponent> next;

    public void ChainTo(CircuitComponent component)
    {
        next = next ?? new List<CircuitComponent>();
        next.Add(component);
    }

    public override void Spark()
    {
        if (next != null)
        {
            foreach (CircuitComponent component in next)
            {
                component.enabled = true;
            }
        }

        enabled = false;
    }

    public override void Spark(string name)
    {
        Spark();
    }

    public override IEnumerable<Vector3> GetWireEndpoints()
    {
        foreach (CircuitComponent component in next)
        {
            yield return component.transform.position;
        }
    }
}
