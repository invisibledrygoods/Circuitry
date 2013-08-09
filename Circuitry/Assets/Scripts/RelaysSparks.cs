using UnityEngine;
using System.Collections;
using Require;

public class RelaysSparks : SimpleCircuitComponent
{
    void OnEnable()
    {
        Spark("next");
    }

    void OnDrawGizmos()
    {
        DrawWires();

        if (enabled)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.color = Color.white;
        }

        Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
    }
}
