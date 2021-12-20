using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private new Rigidbody rigidbody;

    private void Update()
    {
        if (transform.position.magnitude > 150f)
            Destroy(gameObject);
    }

    public void Init(Vector3 direction)
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.forward = direction.normalized;
        rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}
