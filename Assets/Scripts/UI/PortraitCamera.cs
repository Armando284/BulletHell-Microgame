using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = new Vector3(target.position.x, target.position.y, Camera.main.transform.position.z);
    }
}
