using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float zoomSpeed = 1.0f;
    public Vector2 zoomLimits = new Vector2(10, 60);

    private Vector3 moveInput;
    private float zoomInput;
    public float zoomValue = 0f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        zoomInput = Input.GetAxis("Mouse ScrollWheel");
    }

    private void LateUpdate()
    {
        transform.position += movementSpeed * Time.deltaTime * moveInput;
        //mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView += zoomSpeed * -zoomInput, zoomLimits.x, zoomLimits.y);

        zoomValue += zoomInput * 10;
        if (zoomValue >= zoomLimits.x && zoomValue <= zoomLimits.y)
        {
            transform.position += zoomInput * transform.forward;
        }
        zoomValue = Mathf.Clamp(zoomValue, zoomLimits.x, zoomLimits.y);
    }
}
