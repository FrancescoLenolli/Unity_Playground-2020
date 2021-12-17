using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAsteroids
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();
        [SerializeField] private bool randomSpawn = true;
        private Action<Vector3> actionSpawn;
        private int sequentialIndex = 0;

        private void Awake()
        {
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
            SpawnObject(spawnableObjects[randomIndex], spawnPosition);
        }

        private void SequentialSpawn(Vector3 spawnPosition)
        {
            SpawnObject(spawnableObjects[sequentialIndex], spawnPosition);
            sequentialIndex = ++sequentialIndex % spawnableObjects.Count;
        }

        private void SpawnObject(GameObject spawnObject, Vector3 spawnPosition)
        {
            Instantiate(spawnObject, spawnPosition, spawnObject.transform.rotation);
        }
    }
}