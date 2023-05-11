using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    #region Singleton
    public static MapCreator Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Floor nodes")]
    public List<FloorNode> floorNodes;
    [Header("Object nodes")]
    public List<EnvironmentNode> spawnNodes;
    [Header("Level layers")]
    public Transform ground;
    public Transform interactable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject CreateNode(Vector3 position, GameObject node, Transform parent, Vector3 scale)
    {
        GameObject newNode = Instantiate<GameObject>(node, position, Quaternion.identity);
        newNode.transform.parent = parent;
        newNode.transform.localScale = scale;
        return newNode;
    }

    public GameObject CreateFloorNode(Vector3 position, FloorNodeType nodeType, float cellSize)
    {
        FloorNode node = floorNodes.Find((FloorNode obj) => obj.type == nodeType);
        if (node == null)
            return null;
        return CreateNode(position, node.node, ground, Vector3.one * cellSize);
    }

    public GameObject CreateSpawnNode(Vector3 position, EnvironmentNodeType nodeType, float cellSize)
    {
        EnvironmentNode node = spawnNodes.Find((EnvironmentNode obj) => obj.type == nodeType);
        if (node == null)
            return null;
        return CreateNode(position, node.node, interactable, Vector3.one * cellSize);
    }

    public void DestroyNode(GameObject nodeObject)
    {
        Destroy(nodeObject, .1f);
    }
}

public enum FloorNodeType { Grass, Sand, Water, Forest }

public enum EnvironmentNodeType { None, Wall, Tree, BigTree, SceneEntry, SceneExit }