using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private LayerMask interactiveObjectsMask;
    static RaycastHit hit;
    
    void Update()
    {
        Vector3 forward = Camera.transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(Camera.transform.position, forward, Color.green);
    }
}
