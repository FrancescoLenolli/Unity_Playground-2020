using UnityEngine;

namespace CoreCharacter
{
    public enum InputType { XZAxis, XYAxis, XAxis }

    [CreateAssetMenu(menuName = "CharacterValues", fileName = "NewValues")]
    public class CharacterValues : ScriptableObject
    {
        [Tooltip("On what axis the character moves on.")]
        public InputType inputType = InputType.XZAxis;
        [Tooltip("Movement speed of the Character.")]
        public float speed = 1.0f;
        [Tooltip("Strength of the Character's Jump.")]
        public float jumpForce = 1.0f;
        [Tooltip("Gravity Force that affects this character.")]
        public float gravityScale = 5.0f;
        [Tooltip("Does the character accelerate when moving or moves at a constant speed?")]
        public bool movementWithAcceleration = true;
    }
}
