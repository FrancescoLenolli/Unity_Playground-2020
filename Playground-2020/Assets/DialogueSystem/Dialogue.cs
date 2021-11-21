using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/DialogueSystem/Dialogue", fileName = "NewDialogue")]
public class Dialogue : ScriptableObject
{
    public enum CharacterStatus { Normal, Angry }

    [System.Serializable]
    public struct DialogueSection
    {
        public string characterName;
        public CharacterStatus status;
        [TextArea]
        public string text;
    }

    public List<DialogueSection> sections = new List<DialogueSection>();
}
