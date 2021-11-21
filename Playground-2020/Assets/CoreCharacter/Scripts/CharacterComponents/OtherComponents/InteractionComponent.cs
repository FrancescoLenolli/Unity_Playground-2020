using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreCharacter
{
    public class InteractionComponent : CharacterComponent
    {
        [SerializeField]
        private float interactionRange = 1f;
        [SerializeField]
        private bool canInteract = true;

        public void SetInteraction(bool canInteract)
        {
            this.canInteract = canInteract;
        }

        public bool IsEnabled()
        {
            return canInteract;
        }

        public float GetRange()
        {
            return interactionRange;
        }

    }
}
