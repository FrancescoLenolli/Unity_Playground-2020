using ObjectPool;
using UnityEngine;

namespace DemoAsteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] Vector2 speedRange = Vector2.one;
        [SerializeField] Vector2 startingVelocityRange = Vector2.one;

        private new Rigidbody rigidbody;
        private ObjectPool<Asteroid> objectPool = new ObjectPool<Asteroid>();
        private float speed;
        private Vector3 direction;
        private float startingHeight;
        private bool canMove = false;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!canMove)
                return;

            transform.localPosition = new Vector3
                (transform.localPosition.x, startingHeight, transform.localPosition.z);

            if (transform.position.magnitude >= 150f)
            {
                canMove = false;
                rigidbody.isKinematic = true;
                objectPool.CollectItem(this);
            }
        }

        private void FixedUpdate()
        {
            if (canMove)
                rigidbody.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.Force);
        }

        public void Init(ObjectPool<Asteroid> objectPool, Vector3 startingPosition)
        {
            if (this.objectPool != objectPool)
                this.objectPool = objectPool;

            transform.position = startingPosition;
            startingHeight = transform.position.y;
            speed = Random.Range(speedRange.x, speedRange.y);
            Vector3 randomPositionInsideCamera = Camera.main.ScreenToWorldPoint
                (new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));

            direction = (randomPositionInsideCamera - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);

            canMove = true;
            rigidbody.isKinematic = false;
        }
    }
}