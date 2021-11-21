using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected CharacterControl player;
    private InteractionComponent playerInteraction;

    private void Awake()
    {
        player = FindObjectOfType<CharacterControl>();
        playerInteraction = player.GetComponent<InteractionComponent>();
    }

    private void Update()
    {
        if (!playerInteraction)
            return;

        bool isPlayerInRange = CharacterUtilities.IsTargetInRange(transform, player.transform, playerInteraction.GetRange());
        bool canInteract = isPlayerInRange && Input.GetButtonDown("Jump");

        Debug.Log(isPlayerInRange);

        if (canInteract)
            Interact();
    }

    protected virtual void Interact() { }
}
