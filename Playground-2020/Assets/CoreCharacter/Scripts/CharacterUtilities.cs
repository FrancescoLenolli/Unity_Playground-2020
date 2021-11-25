using UnityEngine;

namespace CoreCharacter.Utilities
{
    public static class CharacterUtilities
    {
        /// <summary>
        /// Returns TRUE if the given Transform is touching the ground using a Raycast.
        /// </summary>
        public static bool IsGrounded(Transform transform)
        {
            float offset = 0.1f;
            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
            RaycastHit[] hitsInfo = Physics.RaycastAll(startPoint, Vector3.down, offset * 2);
            foreach (RaycastHit hit in hitsInfo)
            {
                if (hit.transform != transform)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns TRUE if the given BoxCollider is touching the ground.
        /// </summary>
        public static bool IsGrounded(BoxCollider boxCollider)
        {
            float offset = 0.1f;
            RaycastHit[] hitsInfo = Physics.BoxCastAll(boxCollider.bounds.center, boxCollider.bounds.size, Vector3.down, Quaternion.identity, offset);
            foreach (RaycastHit hitInfo in hitsInfo)
            {
                if (hitInfo.transform != boxCollider.transform)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns TRUE if the given SphereCollider is touching the ground.
        /// </summary>
        public static bool IsGrounded(SphereCollider sphereCollider)
        {
            float offset = 0.1f;
            RaycastHit[] hitsInfo = Physics.SphereCastAll(sphereCollider.bounds.center, sphereCollider.radius, Vector3.down, offset);
            foreach (RaycastHit hitInfo in hitsInfo)
            {
                if (hitInfo.transform != sphereCollider.transform)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns TRUE if the given CapsuleCollider is touching the ground.
        /// </summary>
        public static bool IsGrounded(CapsuleCollider capsuleCollider)
        {
            float offset = 0.1f;
            Vector3 centerWorldPosition = capsuleCollider.bounds.center;
            Vector3 originPoint = new Vector3(centerWorldPosition.x, centerWorldPosition.y - (capsuleCollider.height / 4), centerWorldPosition.z);
            RaycastHit[] hitsInfo = Physics.SphereCastAll(originPoint, capsuleCollider.radius, Vector3.down, offset);
            foreach (RaycastHit hitInfo in hitsInfo)
            {
                if (hitInfo.transform != capsuleCollider.transform)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns TRUE if the given rigidbody is falling down.
        /// </summary>
        public static bool IsFalling(Rigidbody rigidbody)
        {
            if (!rigidbody)
            {
                Debug.Log($"{rigidbody.name} missing Rigidbody component");
                return false;
            }

            return rigidbody.velocity.y < 0.0f;
        }

        /// <summary>
        /// Return squared distance between two objects.
        /// Avoid costly squared calculation.
        /// </summary>
        public static float SqrDistance(Transform character, Transform target)
        {
            Vector3 offset = target.position - character.position;

            return offset.sqrMagnitude;
        }

        public static float SqrDistance(Transform character, Vector3 target)
        {
            Vector3 offset = target - character.position;

            return offset.sqrMagnitude;
        }

        /// <summary>
        /// Return true if the target is within a certain range from the character.
        /// </summary>
        public static bool IsTargetInRange(Transform character, Transform target, float range)
        {
            return SqrDistance(character, target) < range * range;
        }

        public static bool IsTargetInRange(Transform character, Vector3 target, float range)
        {
            return SqrDistance(character, target) < range * range;
        }

        /// <summary>
        /// Try to get a gameobject component, Add it on the fly if missing.
        /// </summary>
        public static T TryGetComponent<T>(GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (!component)
                component = gameObject.AddComponent<T>();

            return component;
        }
    }
}
