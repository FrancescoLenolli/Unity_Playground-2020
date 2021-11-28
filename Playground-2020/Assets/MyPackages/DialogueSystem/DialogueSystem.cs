using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueSystem : Singleton<DialogueSystem>
{
    [SerializeField]
    private GameObject dialogueCanvas = null;
    [SerializeField]
    private TextMeshProUGUI dialogueSection = null;
    [SerializeField]
    private TextMeshProUGUI characterName = null;
    [SerializeField]
    private float dialogueDelay = 1f;

    private Dialogue currentDialogue;
    private bool canAutoplay = false;
    private bool isCurrentDialogueEnded = true;

    public void SetDialogue(Dialogue newDialogue)
    {
        if (!isCurrentDialogueEnded)
            return;

        currentDialogue = newDialogue;
        StartDialogue();
    }

    public void AutoPlayDialogue()
    {
        canAutoplay = !canAutoplay;
    }

    private void StartDialogue()
    {
        dialogueCanvas.SetActive(true);
        StartCoroutine(ShowDialogueRoutine());
    }

    private void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
    }

    private IEnumerator ShowDialogueRoutine()
    {
        int index = 0;
        int lastIndex = currentDialogue.sections.Count;
        bool canEndDialogue = false;

        while (!canEndDialogue)
        {
            isCurrentDialogueEnded = false;
            bool isDialogueEnded = index >= lastIndex;

            if (!isDialogueEnded)
            {
                characterName.text = currentDialogue.sections[index].characterName;
                dialogueSection.text = dialogueSection.text.Remove(0);
                foreach (char character in currentDialogue.sections[index].text)
                {
                    dialogueSection.text += character;
                    yield return new WaitForSeconds(0.05f);
                }
                ++index;
            }

            if (!canAutoplay)
            {
                while (!Input.GetKeyDown(KeyCode.Space) && !canAutoplay)
                { yield return null; }
            }
            else if (!isDialogueEnded)
            {
                yield return new WaitForSeconds(dialogueDelay);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isDialogueEnded)
            {
                canEndDialogue = true;
                EndDialogue();
            }

            yield return null;
        }

        isCurrentDialogueEnded = true;
        yield return null;
    }
}
