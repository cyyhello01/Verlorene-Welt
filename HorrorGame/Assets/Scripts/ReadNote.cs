using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ReadNote : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public GameObject noteUI;
    //public MouseLook mouseLook;

    public GameObject pickUpText;
    public GameObject closeNoteText;
    public AudioClip pickUpSound;
    public AudioClip putDownSound;

    public bool isCollide;

    // Start is called before the first frame update
    void Start()
    {
        //noteUI.SetActive(false);
        pickUpText.SetActive(false);

        isCollide = false;
    }

    void OnTriggerEnter(Collider other) //&& noteUI.activeSelf
    {
        if (other.gameObject.tag == "Collide" && !noteUI.activeSelf)
        {
            isCollide = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Collide")
        {
            isCollide = false;
            pickUpText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && isCollide) //when note is not active
        {
            Debug.Log("Pressed F");
           // mouseLook.enabled = false;

            noteUI.SetActive(true);
            Debug.Log("note displayed");

            closeNoteText.SetActive(true);
            pickUpText.SetActive(false);
            playPickUpSound();


            //player.GetComponent<FirstPersonController>();
            //Debug.Log("freeze walk");

            //cam.GetComponent<Camera>().enabled = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButtonDown("NotInteract") && noteUI.activeSelf) //to check whether X is pressed & noteUI is true (active)
        {
            Debug.Log("Note is active");
            //mouseLook.enabled = true;

            noteUI.SetActive(false);
   
            Debug.Log("disable note");
            closeNoteText.SetActive(false);
            playPutDownSound();

        }

    }

    public void BackButton()
    {
        noteUI.SetActive(false);
        cam.GetComponent<Camera>().enabled = true;
    }

    void playPickUpSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = pickUpSound;
        audio.volume = 1.0f;
        audio.Play();
    }

    void playPutDownSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = putDownSound;
        audio.volume = 0.3f;
        audio.Play();
    }
}
