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
    private Camera mainCamera;
    private Vector2 positionXLimit;
    private Vector2 positionZLimit;

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
        //float positionX = Mathf.Clamp(transform.position.x, positionXLimit.x, positionXLimit.y);
        //float positionZ = Mathf.Clamp(transform.position.z, positionZLimit.x, positionZLimit.y);
        //transform.position = new Vector3(positionX, transform.position.y, positionZ);

        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView += zoomSpeed * -zoomInput, zoomLimits.x, zoomLimits.y);
    }

    public void SetCameraLimits(Vector3 lowLimit, Vector3 highLimit)
    {
        positionXLimit = new Vector2(lowLimit.x, highLimit.x);
        positionZLimit = new Vector2(lowLimit.z, highLimit.z);
    }
}
