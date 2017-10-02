﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{

    public bool walkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;

    public node parent;

    public node(bool _walkable, Vector3 _worldpos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldpos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int FCost
    {
        get { return gCost + hCost; }
    }
}
