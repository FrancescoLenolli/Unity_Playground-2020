using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Vector2 speedRange = Vector2.one;
    [SerializeField] Vector2 startingVelocityRange = Vector2.one;
    private float speed;
    private Vector3 direction;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = Random.Range(speedRange.x, speedRange.y);
        Vector3 randomPositionInsideCamera = Camera.main.ScreenToWorldPoint
            (new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));

        direction = (randomPositionInsideCamera - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z);
    }

    private void Update()
    {
        if (transform.position.magnitude >= 150f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.Force);
    }
}
