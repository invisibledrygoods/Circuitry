using UnityEngine;
using System.Collections.Generic;
using Require;

public abstract class CircuitComponent : MonoBehaviour
{
    public abstract void Spark();
    public abstract void Spark(string name);
    public abstract IEnumerable<Vector3> GetWireEndpoints();

    public void DrawWires()
    {
        foreach (Vector3 endpoint in GetWireEndpoints())
        {
            float distance = Vector3.Distance(transform.position, endpoint);
            for (float i = 0; i < distance; i++)
            {
                float angle = i * Mathf.PI / 180.0f / distance;

                Vector3 start = Vector3.Lerp(transform.position, endpoint, i / distance);
                Vector3 startRight = start + Vector3.Cross((transform.position - endpoint), Camera.current.transform.forward).normalized * 0.06f;
                Vector3 startLeft = start - Vector3.Cross((transform.position - endpoint), Camera.current.transform.forward).normalized * 0.06f;
                Vector3 end = Vector3.Lerp(transform.position, endpoint, (i + 0.6f) / distance); 
                Gizmos.DrawLine(startRight, end);
                Gizmos.DrawLine(startLeft, end);
                Gizmos.DrawLine(startRight, startLeft);
            }
        }
    }
}
