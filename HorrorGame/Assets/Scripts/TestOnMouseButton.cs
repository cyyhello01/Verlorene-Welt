using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnMouseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse is clicked");
        Debug.Log("reset");
        Debug.Log("reset2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
