using CoreCharacter;
using ObjectPool;
using UnityEngine;

namespace DemoAsteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Vector2 speedRange = Vector2.one;
        [SerializeField] private Vector2 startingVelocityRange = Vector2.one;
        [SerializeField] private GameObject explosionParticlePrefab = null;

        private new Rigidbody rigidbody;
        private new Collider collider;
        private ObjectPool<Asteroid> objectPool = null;
        private float speed;
        private Vector3 direction = Vector3.zero;
        private float startingHeight;
        private bool canMove = false;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            SetMove(false);
        }

        private void Update()
        {
            if (!canMove)
                return;

            transform.localPosition = new Vector3
                (transform.localPosition.x, startingHeight, transform.localPosition.z);

            if (transform.position.magnitude >= 150f)
            {
                Dispose();
            }
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                rigidbody.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.VelocityChange);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject)
                return;

            CharacterControl player = collision.gameObject.GetComponent<CharacterControl>();
            if (player)
            {
                //player.gameObject.SetActive(false);
                return;
            }

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet)
            {
                Destroy(bullet.gameObject);
                Instantiate(explosionParticlePrefab, transform.position, explosionParticlePrefab.transform.rotation);
                Dispose();
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
                new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            rigidbody.AddTorque(randomTorque * 2f);

            SetMove(true);
        }

        private void SetMove(bool canMove)
        {
            this.canMove = canMove;
            collider.enabled = canMove;
            rigidbody.isKinematic = !canMove;
        }

        private void Dispose()
        {
            SetMove(false);
            objectPool?.CollectItem(this);
        }
    }
}