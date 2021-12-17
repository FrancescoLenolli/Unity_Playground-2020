using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class RigidbodyJump : JumpComponent
    {
        private new Rigidbody rigidbody;
        private CapsuleCollider capsuleCollider;

        private void FixedUpdate()
        {
            ScaleGravity();
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);

            rigidbody = CharacterUtilities.TryGetComponent<Rigidbody>(character.gameObject);
            capsuleCollider = character.GetComponent<CapsuleCollider>();
            rigidbody.freezeRotation = true;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        protected override void Jump()
        {
            if (!canJump)
                return;

            rigidbody.AddForce(Vector3.up * characterValues.jumpForce, ForceMode.Impulse);
            canJump = false;
        }

        protected override bool CanJump()
        {
            return isGrounded;
        }

        protected override bool IsGrounded()
        {
            return CharacterUtilities.IsGrounded(capsuleCollider);
        }

        protected override bool IsFalling()
        {
            return rigidbody.velocity.y < 0;
        }

        /// <summary>
        /// Add force to Rigidbody to simulate a higher gravity scale.
        /// Makes the jump less floaty.
        /// </summary>
        private void ScaleGravity()
        {
            // Doubles gravity when falling down to fall faster. Prevents weird floating motions.
            float gravityScale = IsFalling() ? characterValues.gravityScale * 2 : characterValues.gravityScale;
            rigidbody.AddForce(Physics.gravity * (gravityScale - 1) * rigidbody.mass);
        }
    }
}
