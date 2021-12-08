using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : InteractableObject
{
    [SerializeField]
    private Dialogue dialogue = null;

    protected override void Interact()
    {
        DialogueSystem.Instance.SetDialogue(dialogue);
    }
}
