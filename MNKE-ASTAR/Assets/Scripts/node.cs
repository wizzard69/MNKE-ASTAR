using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{

    public bool walkable;
    public Vector3 worldPosition;

    public node(bool _walkable, Vector3 _worldpos)
    {
        walkable = _walkable;
        worldPosition = _worldpos;
    }
}
