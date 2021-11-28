using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    [RequireComponent(typeof(InputComponent))]
    public class CharacterControl : MonoBehaviour
    {
        public CharacterValues characterValues = null;
        [HideInInspector]
        public InputComponent inputComponent;

        private void Awake()
        {
            inputComponent = GetComponent<InputComponent>();
            CharacterComponent[] components = GetComponents<CharacterComponent>();
            foreach(CharacterComponent component in components)
            {
                component.SetUp(this);
            }
        }
    }
}
