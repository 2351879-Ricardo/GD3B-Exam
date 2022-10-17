using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour {

    public List<NavMeshSurface> surfaces;

    // Use this for initialization
    public void UpdateNavMesh()
    {
        foreach (var t in surfaces)
        {
            t.BuildNavMesh();
        }
    }

}