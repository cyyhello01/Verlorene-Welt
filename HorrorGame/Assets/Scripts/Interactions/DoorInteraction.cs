using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private string unlockedPromptText = "Press [F] to open door";
    [SerializeField] private string lockedPromptText = "The door seems to be locked";
    private string boolText = "isDoorOpen";
    private string unlockedDoorTag = "UnlockedDoor";
    private string lockDoorTag = "LockedDoor";
    
    private bool isDoorOpen = false;
    private GameObject currentDoor;

    [SerializeField] private float doorOpenTime = 3.0f;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;


    private void Start()
    {
    }

    public void OpenDoor(RaycastHit hit)
    {
        if (hit.collider.gameObject.tag == unlockedDoorTag && isDoorOpen == false)
        {
            currentDoor = hit.collider.gameObject;
            InteractionPromptUI.promptTextMessage = unlockedPromptText;
            InteractionPromptUI.textOn = true;
            Door(doorOpenSound, true, true, currentDoor);
        }
        else if (hit.collider.gameObject.tag == lockDoorTag && isDoorOpen == false)
        {
            InteractionPromptUI.promptTextMessage = lockedPromptText;
            InteractionPromptUI.textOn = true;
        }
    }

    public void CloseDoor(float doorTimer)
    {
        InteractionPromptUI.textOn = false;
        InteractionPromptUI.promptTextMessage = "";

        if (isDoorOpen && doorTimer > doorOpenTime)
        {
            Door(doorCloseSound, false, false, currentDoor);
            currentDoor = null;
        }
    }

    public void Door(AudioClip audioClip, bool openCheck, bool doorIsOpen, GameObject thisDoor)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioClip;
        audio.Play();
        isDoorOpen = openCheck;
        thisDoor.GetComponent<Animator>().SetBool(boolText, doorIsOpen);
    }
}
