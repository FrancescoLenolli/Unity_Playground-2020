using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAsteroids
{
    public class SetMeshCollider : MonoBehaviour
    {
        [Tooltip("Object that holds the mesh to retrieve.")]
        [SerializeField] private GameObject model;

        private void Awake()
        {
            MeshFilter meshFilter = model.GetComponent<MeshFilter>();
            MeshCollider meshCollider = GetComponent<MeshCollider>();

            meshCollider.sharedMesh = meshFilter.mesh;
        }

    }
}
