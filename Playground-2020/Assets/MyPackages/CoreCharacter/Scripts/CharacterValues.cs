using UnityEngine;

namespace CoreCharacter
{
    public enum InputType { XZAxis, XYAxis, XAxis }

    [CreateAssetMenu(menuName = "CharacterValues", fileName = "NewValues")]
    public class CharacterValues : ScriptableObject
    {
        public InputType inputType = InputType.XZAxis;
        public float speed = 1.0f;
        public float jumpForce = 1.0f;
        public float gravityScale = 5.0f;
    }
}
