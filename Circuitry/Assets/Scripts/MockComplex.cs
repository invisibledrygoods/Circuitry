using UnityEngine;
using System.Collections.Generic;
using Require;

public class MockComplex : CircuitComponent
{
    public List<CircuitComponent> lol;
    public List<CircuitComponent> dongs;
    public List<GameObject> objects;

    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.14f);
        DrawWires();
    }
}
