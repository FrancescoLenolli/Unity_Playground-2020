using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAsteroids
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Asteroid> spawnableObjects = new List<Asteroid>();
        [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
        [SerializeField] private float spawnTime = 1f;
        [SerializeField] private bool randomSpawn = true;
        [SerializeField] private Transform objectPoolContainer = null;
        [SerializeField] private Transform player = null;

        private Action<Vector3> actionSpawn;
        private List<ObjectPool<Asteroid>> objectPools = new List<ObjectPool<Asteroid>>();
        private int sequentialIndex = 0;

        private void Awake()
        {
            for (int i = 0; i < spawnableObjects.Count; i++)
            {
                ObjectPool<Asteroid> objectPool = ObjectPool<Asteroid>.Create(spawnableObjects[i], objectPoolContainer, 5);
                objectPools.Add(objectPool);
            }

            SelectSpawnMethod();
            StartCoroutine(SpawnRoutine());
        }

        private void SpawnAtRandomPosition()
        {
            int randomIndex = UnityEngine.Random.Range(0, spawnPositions.Count);
            Spawn(spawnPositions[randomIndex].position);
        }

        private void Spawn(Vector3 spawnPosition)
        {
            actionSpawn?.Invoke(spawnPosition);
        }

        private void SelectSpawnMethod()
        {
            if (spawnableObjects.Count == 0)
            {
                Debug.LogWarning($"No Gameobjects spawnable in Spawner {name}!");
                return;
            }

            if (randomSpawn)
                actionSpawn = (spawnPosition) => RandomSpawn(spawnPosition);
            else
                actionSpawn = (spawnPosition) => SequentialSpawn(spawnPosition);
        }

        private void RandomSpawn(Vector3 spawnPosition)
        {
            int randomIndex = UnityEngine.Random.Range(0, spawnableObjects.Count);
            SpawnObject(randomIndex, spawnPosition);
        }

        private void SequentialSpawn(Vector3 spawnPosition)
        {
            SpawnObject(sequentialIndex, spawnPosition);
            sequentialIndex = ++sequentialIndex % spawnableObjects.Count;
        }

        private void SpawnObject(int spawnIndex, Vector3 spawnPosition)
        {
            Asteroid newAsteroid = objectPools[spawnIndex].GiveItem();
            newAsteroid.Init(objectPools[spawnIndex], spawnPosition, player.position);
        }

        private IEnumerator SpawnRoutine()
        {
            float currentTimer = 0f;

            while(true)
            {
                currentTimer -= Time.deltaTime;
                if(currentTimer <= 0f)
                {
                    SpawnAtRandomPosition();
                    currentTimer = spawnTime;
                }

                yield return null;
            }
        }
    }
}