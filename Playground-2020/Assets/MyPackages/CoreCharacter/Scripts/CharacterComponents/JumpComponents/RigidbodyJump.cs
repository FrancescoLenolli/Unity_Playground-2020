using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class RigidbodyJump : JumpComponent
    {
        private new Rigidbody rigidbody;

        private void FixedUpdate()
        {
            ScaleGravity();
            HandleJump();
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);

            rigidbody = CharacterUtilities.TryGetComponent<Rigidbody>(character.gameObject);
            rigidbody.freezeRotation = true;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            characterValues = character.characterValues;
        }

        protected override void HandleJump()
        {
            bool alreadyJumped = isJumping && canJump && rigidbody.useGravity;

            if (canJump)
            {
                Jump();
            }
            else if (alreadyJumped)
            {
                isJumping = false;
            }
        }

        protected override void Jump()
        {
            rigidbody.AddForce(Vector3.up * characterValues.jumpForce, ForceMode.Impulse);
            isJumping = true;
            canJump = false;
        }

        protected override bool IsGrounded()
        {
            return CharacterUtilities.IsGrounded(GetComponent<CapsuleCollider>());
        }

        /// <summary>
        /// Add force to Rigidbody to simulate a higher gravity scale.
        /// Makes the jump less floaty.
        /// </summary>
        private void ScaleGravity()
        {
            rigidbody.AddForce(Physics.gravity * (characterValues.gravityScale - 1) * rigidbody.mass);
        }
    }
}
