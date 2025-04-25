using UnityEngine;
using TMPro;

public class MainQuestText : MonoBehaviour
{
    public TextMeshProUGUI questText;

void Start()
{
    if (questText != null && QuestManager.Instance != null)
    {
        questText.text = QuestManager.Instance.currentQuest;
    }
}
}
