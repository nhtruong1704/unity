using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistanceGeneration : MonoBehaviour
{
    NavMeshSurface navMeshSurFace;

    private void Start()
    {
        navMeshSurFace = GetComponent<NavMeshSurface>();
        navMeshSurFace.BuildNavMesh();
    }
}
