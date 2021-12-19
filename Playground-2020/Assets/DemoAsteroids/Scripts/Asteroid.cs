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
        private Vector3 direction = Vector3.zero;
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
            {
                rigidbody.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.VelocityChange);
            }
        }

        public void Init(ObjectPool<Asteroid> objectPool, Vector3 startingPosition, Vector3 targetPosition)
        {
            if (this.objectPool != objectPool)
                this.objectPool = objectPool;

            transform.position = startingPosition;
            startingHeight = transform.position.y;
            speed = Random.Range(speedRange.x, speedRange.y);

            direction = (targetPosition - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);

            Vector3 randomTorque = 
                new Vector3 (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            rigidbody.AddTorque(randomTorque * 2f);

            rigidbody.isKinematic = false;
            canMove = true;
        }
    }
}