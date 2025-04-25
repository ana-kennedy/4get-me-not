using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue System/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [TextArea(5, 10)]
    public string dialogueText; // Holds the paragraph for the Typewriter
}