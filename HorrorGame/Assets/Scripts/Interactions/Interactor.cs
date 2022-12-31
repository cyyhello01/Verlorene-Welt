using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Serialization;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 5.0f;
    [SerializeField] private Camera playerCamera;
    
    private Vector3 screenCenter;
    private GameObject currentTarget;

    private int interactableObjMask;
    private int highlightedObjMask;

    private string interactableObjMaskName = "InteractableObjects";
    private string highlightedObjMaskName = "HighlightedObjects";

    private DoorInteraction _doorInteraction;
    private float timer = 0.0f;

    private void Awake()
    {
        _doorInteraction = gameObject.AddComponent<DoorInteraction>();
        screenCenter = new Vector3(Screen.width >> 1, Screen.height >> 1);
        interactableObjMask = LayerMask.NameToLayer(interactableObjMaskName);
        highlightedObjMask = LayerMask.NameToLayer(highlightedObjMaskName);
    }

    private void Update()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(playerCamera.ScreenPointToRay(screenCenter), out hitInfo, interactionRange,
                LayerMask.GetMask(interactableObjMaskName, highlightedObjMaskName)))
        {
            GameObject target = hitInfo.collider.gameObject;

            if (currentTarget != target)
            {
                currentTarget = target;
                currentTarget.layer = highlightedObjMask;

                _doorInteraction.OpenDoor(hitInfo);
            }
        }
        else if (currentTarget != null)
        {
            currentTarget.layer = interactableObjMask;
            currentTarget = null;

            timer += Time.deltaTime;
            _doorInteraction.CloseDoor(timer);
            if (timer > 3.0f)
                timer = 0.0f;
        }
    }
}
