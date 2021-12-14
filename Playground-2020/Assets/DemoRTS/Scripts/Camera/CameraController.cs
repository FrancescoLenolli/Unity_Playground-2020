using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoRTS.CameraControls
{
    public class CameraController : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public float rotationSpeed = 1f;
        public float zoomSpeed = 1f;
        public Vector2 cameraYLimits = new Vector2(6f, 20f);

        private Vector3 moveInput;
        private float rotationInput;
        private float zoomInput;
        private Camera mainCamera;
        private Vector3 cameraStartingPosition;

        private void Awake()
        {
            mainCamera = Camera.main;
            cameraStartingPosition = mainCamera.transform.position;
        }

        private void Update()
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rotationInput = Input.GetAxis("Rotate");
            zoomInput = Input.GetAxis("Mouse ScrollWheel");
        }

        private void LateUpdate()
        {
            transform.Translate(movementSpeed * Time.deltaTime * moveInput);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * rotationInput);

            Vector3 cameraZoomDirection = mainCamera.transform.forward;
            Vector3 cameraCurrentPosition = mainCamera.transform.position;
            Vector3 cameraNewPosition = cameraCurrentPosition += zoomSpeed * Time.deltaTime * zoomInput * cameraZoomDirection;

            if (cameraNewPosition.y > cameraYLimits.x && cameraNewPosition.y < cameraYLimits.y)
                mainCamera.transform.position = cameraNewPosition;
        }
    }
}