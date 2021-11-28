using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterComponent : MonoBehaviour
    {
        protected CharacterValues characterValues;

        public virtual void SetUp(CharacterControl character) { characterValues = character.characterValues; }
    }
}
