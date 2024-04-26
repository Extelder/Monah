using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Point : MonoBehaviour
{
    [field: SerializeField] public Path[] _paths;
    [field: SerializeField] public Colony Colony { get; private set; }

    public Action<Unit> UnitEntered;
    
    private void Awake()
    {
        _paths = GetComponents<Path>();
    }

    public Vector2 GetPosition() => transform.position;

    public Path GetRandomPath(Unit unit)
    {
        List<Path> pathsWithoutStart = new List<Path>();
        for (int i = 0; i < _paths.Length; i++)
        {
            if (_paths[i].EndPoint != unit.StartPoint)
            {
                pathsWithoutStart.Add(_paths[i]);
            }
        }
        
        return pathsWithoutStart[Random.Range(0, pathsWithoutStart.Count)];
    }

    public Path FindClosestToPointPath(Point point)
    {
        Path closestPath = _paths[Random.Range(0, _paths.Length)];
        float closestDistance = Vector2.Distance(closestPath.EndPoint.GetPosition(), point.GetPosition());
        for (int i = 0; i < _paths.Length; i++)
        {
            if (Vector2.Distance(_paths[i].EndPoint.GetPosition(), point.GetPosition()) < closestDistance)
            {
                closestPath = _paths[i];
            }
        }

        return closestPath;
    }
}