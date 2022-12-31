using System;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;
using VerloreneWelt.Manager;

namespace VerloreneWelt.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        // Camera Rotation Limit
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        
        private Rigidbody playerRigidbody;
        private InputManager inputManager;

        private Animator animator;
        private bool hasAnimator;
        
        // Player velocity hash
        private int xVelocityHash;
        private int yVelocityHash;

        // Camera rotation
        private float xRotation;

        private const float walkSpeed = 2.0f;
        private const float runSpeed = 4.0f;
        private Vector2 currentVelocity;

        // Start is called before the first frame update
        void Start()
        {
            hasAnimator = TryGetComponent<Animator>(out animator);
            playerRigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();

            xVelocityHash = Animator.StringToHash("xVelocity");
            yVelocityHash = Animator.StringToHash("yVelocity");
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            CameraMovements();
        }

        private void Move()
        {
            if(!hasAnimator) return;

            float targetSpeed = inputManager.Run ? runSpeed : walkSpeed;

            if (inputManager.Move == Vector2.zero) 
                targetSpeed = 0.1f;

            currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
            currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            var xVelocityDifference = currentVelocity.x - playerRigidbody.velocity.x;
            var zVelocityDifference = currentVelocity.y - playerRigidbody.velocity.z;

            playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelocityDifference, 0, zVelocityDifference)), ForceMode.VelocityChange);

            animator.SetFloat(xVelocityHash, currentVelocity.x);
            animator.SetFloat(yVelocityHash, currentVelocity.y);
        }

        private void CameraMovements()
        {
            if (!hasAnimator)
                return;

            var mouseX = inputManager.Look.x;
            var mouseY = inputManager.Look.y;
            Camera.position = CameraRoot.position;

            xRotation -= mouseY * MouseSensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, UpperLimit, BottomLimit);
            
            Camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up, mouseX * MouseSensitivity * Time.deltaTime);
        }
    }

}
