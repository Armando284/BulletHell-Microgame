using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorNode
{
    public FloorNodeType type;
    public List<FloorNodeType> neighboorTypes;
    public List<SpawnObjectType> environmentTypes;
    public GameObject node;
}

