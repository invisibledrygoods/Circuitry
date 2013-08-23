using UnityEngine;
using System.Collections.Generic;
using Require;

public class RelaysSparks : CircuitComponent
{
    public List<CircuitComponent> next;

    bool spark = false;

    void Update()
    {
        // one frame delay to prevent short circuits
        if (spark)
        {
            Spark(next);
        }

        spark = false;
    }

    void OnEnable()
    {
        spark = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 0.03f);
        DrawWires();
    }
}
