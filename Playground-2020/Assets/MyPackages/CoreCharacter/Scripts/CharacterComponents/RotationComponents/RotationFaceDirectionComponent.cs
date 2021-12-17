using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter
{
    public class RotationFaceDirectionComponent : RotationComponent
    {
        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            character.inputComponent.OnActionMove += Rotate;
        }

        protected override void Rotate(Vector3 rotationInput)
        {
            if (rotationInput == Vector3.zero)
                return;

            Vector3 lookAtPosition = new Vector3(rotationInput.x, 0.0f, rotationInput.z);
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition);

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 1.0f);
        }
    }
}