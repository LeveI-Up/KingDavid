using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    public class Patrol : MonoBehaviour
{

    [SerializeField] Cycle patrolPath = null;

    [SerializeField] private int pointCount;
    [SerializeField] private int currentPointIndex;
    [SerializeField] private int lastPointIndex;
    [SerializeField] Vector3 targetInWorld;
    [SerializeField] float speed = 2f;
    private bool atTarget;

    public void Start()
    {
        pointCount = patrolPath.transform.childCount;
        lastPointIndex = 0;
        currentPointIndex = 1;
    }

    private void Update()
    {
        if (atTarget)
        {
            lastPointIndex = currentPointIndex;
            currentPointIndex = (currentPointIndex + 1) % pointCount;
        }
        atTarget = false;
        targetInWorld = patrolPath.transform.GetChild(currentPointIndex).position;
        Vector3 startNode = patrolPath.transform.GetChild(lastPointIndex).position;
        Vector3 endNode = patrolPath.transform.GetChild(currentPointIndex).position;
        List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations);
        if (shortestPath.Count >= 2)
        {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
        else
        {
            atTarget = true;
        }



    }
}

    