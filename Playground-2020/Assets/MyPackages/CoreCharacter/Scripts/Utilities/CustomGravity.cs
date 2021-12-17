using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter.Utilities
{
    /// <summary>
    /// Replace the standard UnityEngine gravity with a custom one.
    /// </summary>
    public class CustomGravity
    {
        public float GravityScale { get; set; }

        private static float globalGravity = -9.81f;
        private Rigidbody rigidbody;

        public CustomGravity(Rigidbody rb)
        {
            rigidbody = rb;
            rigidbody.useGravity = false;
            GravityScale = 1;
        }

        public void AddGravity()
        {
            Vector3 gravity = globalGravity * GravityScale * Vector3.up;
            rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}