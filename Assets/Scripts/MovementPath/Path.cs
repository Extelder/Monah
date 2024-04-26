using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [field: SerializeField] public Point StartPoint { get; private set; }
    [field: SerializeField] public Point EndPoint { get; private set; }

    private void Start()
    {
        StartPoint = GetComponent<Point>();
    }
}