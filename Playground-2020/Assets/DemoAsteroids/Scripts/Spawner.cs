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
        [SerializeField] private bool randomSpawn = true;
        [SerializeField] private Transform objectPoolContainer = null;
        private Action<Vector3> actionSpawn;
        private List<ObjectPool<Asteroid>> objectPools = new List<ObjectPool<Asteroid>>();
        private int sequentialIndex = 0;

        private void Awake()
        {
            for (int i = 0; i < spawnableObjects.Count; i++)
            {
                ObjectPool<Asteroid> objectPool = ObjectPool<Asteroid>.Create(spawnableObjects[i], objectPoolContainer, 15);
                objectPools.Add(objectPool);
            }
            SelectSpawnMethod();
        }

        public void Spawn(Vector3 spawnPosition)
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
            newAsteroid.Init(objectPools[spawnIndex], spawnPosition);
        }
    }
}