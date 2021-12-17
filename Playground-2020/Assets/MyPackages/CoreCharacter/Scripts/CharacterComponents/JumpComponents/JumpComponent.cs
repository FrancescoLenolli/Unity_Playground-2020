using CoreCharacter.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter
{
    public class JumpComponent : CharacterComponent
    {
        protected bool isGrounded = true;
        protected bool canJump = false;

        private void Update()
        {
            isGrounded = IsGrounded();
            canJump = CanJump();
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            character.inputComponent.OnActionJump += Jump;
        }

        protected virtual void Jump() { }
        protected virtual bool CanJump() { return false; }
        protected virtual bool IsGrounded() { return false; }
        protected virtual bool IsFalling() { return false; }
    }
}
