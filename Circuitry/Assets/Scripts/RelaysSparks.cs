using UnityEngine;
using System.Collections.Generic;
using Require;

public class RelaysSparks : CircuitComponent
{
    public List<CircuitComponent> next;

    void OnEnable()
    {
        Spark(next);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 0.03f);
        DrawWires();
    }
}
