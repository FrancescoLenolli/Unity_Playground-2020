using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAsteroids
{
    public class OutsideCameraSpawner : MonoBehaviour
    {
        [SerializeField] Spawner spawner = null;
        [SerializeField] float spawnTime = 3f;
        private new Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        private void Start()
        {
            InvokeRepeating("SpawnAtRandomPoint", 1f, spawnTime);
        }

        private void SpawnAtRandomPoint()
        {
            spawner.Spawn(GetRandomPointOffCamera());
        }

        private Vector3 GetRandomPointOffCamera()
        {
            Vector3 randomPosition = new Vector3(GetRandomValue(), GetRandomValue(), GetRandomValue());
            Vector3 result = camera.ViewportToWorldPoint(randomPosition);
            result = new Vector3(result.x, 2, result.z);

            return result;
        }

        private float GetRandomValue()
        {
            float value = Random.Range(-1.5f, 1.5f);

            if (value >= -3f && value <= 0)
                value -= .3f;
            if (value > 0 && value <= 1f)
                value += 1f;

            return value;
        }
    }
}