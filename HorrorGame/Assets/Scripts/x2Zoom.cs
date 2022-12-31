using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x2Zoom : MonoBehaviour
{
    public Camera cam;
    public float defaultFov = 60;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Zoom in!");
            cam.fieldOfView = (defaultFov / 2);
        }
        else
        {
            cam.fieldOfView = (defaultFov);
        }
    }
}
