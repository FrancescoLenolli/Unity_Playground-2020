using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter
{
    public class RotationComponent : CharacterComponent
    {
        public bool CanRotate { get; set;}

        protected virtual void Rotate(Vector3 rotationInput) { if (!CanRotate) return; }
    }
}