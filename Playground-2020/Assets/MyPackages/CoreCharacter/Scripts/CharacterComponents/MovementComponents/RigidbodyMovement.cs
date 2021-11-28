using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class RigidbodyMovement : MovementComponent
    {
        private new Rigidbody rigidbody;

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            rigidbody = CharacterUtilities.TryGetComponent<Rigidbody>(character.gameObject);
            rigidbody.freezeRotation = true;
        }

        public override void Move(Vector3 movementInput)
        {
            Vector3 velocity = characterValues.speed * Time.fixedDeltaTime * movementInput;
            Vector3 newPosition = transform.position + velocity;

            rigidbody.MovePosition(newPosition);
        }
    }
}
