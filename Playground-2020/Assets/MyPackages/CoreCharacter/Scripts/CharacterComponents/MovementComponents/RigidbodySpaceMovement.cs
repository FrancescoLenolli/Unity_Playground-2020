using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public enum ThrustDirection { Forward, Back, Up, Down, Right, Left }
    public class RigidbodySpaceMovement : MovementComponent
    {
        [SerializeField] private float maxVelocity = 5f;
        [SerializeField] private ThrustDirection thrustDirection = ThrustDirection.Forward;

        private new Rigidbody rigidbody;

        private void FixedUpdate()
        {
            ClampVelocity();
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            rigidbody = CharacterUtilities.TryGetComponent<Rigidbody>(character.gameObject);
            rigidbody.useGravity = false;
            rigidbody.freezeRotation = true;
            character.inputComponent.OnContinuousActionJump += SetThrust;
        }

        protected virtual void SetThrust(bool canThrust)
        {
            if (canThrust)
                rigidbody.AddForce(characterValues.speed * Time.fixedDeltaTime * GetThrustDirection(), ForceMode.Force);
        }

        private void ClampVelocity()
        {
            if (rigidbody.velocity.magnitude > maxVelocity)
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
        }

        private Vector3 GetThrustDirection()
        {
            switch (thrustDirection)
            {
                case ThrustDirection.Forward:
                    return transform.forward;

                case ThrustDirection.Back:
                    return -transform.forward;

                case ThrustDirection.Up:
                    return transform.up;

                case ThrustDirection.Down:
                    return -transform.up;

                case ThrustDirection.Right:
                    return transform.right;

                case ThrustDirection.Left:
                    return -transform.right;

                default:
                    return Vector3.zero;
            }
        }
    }
}