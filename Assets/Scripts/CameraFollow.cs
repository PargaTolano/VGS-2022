using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offset = -5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
             new Vector3(target.position.x, target.position.y, target.position.z + offset);
    }
}
