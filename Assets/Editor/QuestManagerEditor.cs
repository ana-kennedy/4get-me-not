using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw default UI

        QuestManager questManager = (QuestManager)target;

        GUILayout.Space(10);

        // Button to add a quest
        if (GUILayout.Button("Add Quest From Inspector"))
        {
            questManager.AddQuestFromInspector();
        }

        // Button to remove a quest
        if (GUILayout.Button("Remove Quest From Inspector"))
        {
            questManager.RemoveQuestFromInspector();
        }

        // Button to clear all quests
        if (GUILayout.Button("Clear All Quests"))
        {
            questManager.ClearAllQuests();
        }
    }
}