using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorNode
{
    public FloorNodeType type;
    public List<FloorNodeType> neighboorTypes;
    public List<EnvironmentNodeType> environmentTypes;
    public GameObject node;
}

