using CoreCharacter.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter
{
    public class JumpComponent : CharacterComponent
    {
        protected bool canJump = false;
        protected bool isJumping = false;

        public void SetJump()
        {
            canJump = IsGrounded();
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
            character.inputComponent.OnActionJump += SetJump;
        }

        protected virtual void Jump() { }
        protected virtual void HandleJump() { }
        protected virtual bool IsGrounded() { return false; }
    }
}
