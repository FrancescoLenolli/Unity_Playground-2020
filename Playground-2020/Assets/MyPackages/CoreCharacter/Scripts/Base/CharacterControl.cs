using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterControl : MonoBehaviour
    {
        public CharacterValues characterValues = null;
        [HideInInspector]
        public InputComponent inputComponent;

        private void Awake()
        {
            inputComponent = GetComponent<InputComponent>();
            if(!inputComponent)
            {
                Debug.LogError($"Input Component missing in Character {name}!\n" +
                    $"Without an Input Component the Character will not be able to move.");
                return;
            }

            CharacterComponent[] components = GetComponents<CharacterComponent>();
            foreach(CharacterComponent component in components)
            {
                component.SetUp(this);
            }
        }
    }
}
