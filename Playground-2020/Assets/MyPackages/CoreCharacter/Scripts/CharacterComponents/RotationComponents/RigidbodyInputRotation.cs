using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    /// <summary>
    /// Rigidbody faces RotationInput direction.
    /// </summary>
    public class RigidbodyInputRotation : RotationComponent
    {
        private new Rigidbody rigidbody;

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            rigidbody = CharacterUtilities.TryGetComponent<Rigidbody>(character.gameObject);
            character.inputComponent.OnActionMove += Rotate;
        }

        protected override void Rotate(Vector3 rotationInput)
        {
            base.Rotate(rotationInput);
            if (rotationInput == Vector3.zero)
                return;

            rigidbody.MoveRotation(Quaternion.LookRotation(rotationInput, Vector3.up));

        }
    }
}