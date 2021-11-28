using UnityEngine;

namespace CoreCharacter
{
    public class MovementComponent : CharacterComponent
    {
        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            character.inputComponent.OnActionMove += Move;
        }

        public virtual void Move(Vector3 movementInput) { }
    }
}
