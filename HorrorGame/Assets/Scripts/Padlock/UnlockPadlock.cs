using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPadlock : MonoBehaviour
{
    public bool isCollide;

    public GameObject padlockUnlockText;
    public GameObject padlock;
    public GameObject collider;


    // Start is called before the first frame update
    void Start()
    {
        isCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollide && Input.GetButtonDown("Unlock"))
        {
            Debug.Log("Pressed E to unlock");
            PadlockEnlarge();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collide")
        {
            isCollide = true;
            Debug.Log("Collide enter with padlock");
            padlockUnlockText.SetActive(true);
            Debug.Log("padlock unlock text displayed");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Collide")
        {
            isCollide = false;
            Debug.Log("exit collided");
            padlockUnlockText.SetActive(false);
            Debug.Log("padlock unlock Text disappeared");

        }
    }

    void PadlockEnlarge()
    {
        padlock.SetActive(true);
        Debug.Log("Padlock enlarge");
        collider.SetActive(false);
        Debug.Log("Collider disabled"); // to let cursor able to rotate padlock wheel (with collider, cursor click not functioning)
    }
}
