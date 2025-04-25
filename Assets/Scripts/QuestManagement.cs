using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private TextMeshProUGUI questTextUI;
    private List<string> activeQuests = new List<string>();
    public string currentQuest = "";

    private const string QUESTS_KEY = "ActiveQuests";
    private const string CURRENT_QUEST_KEY = "CurrentQuest";

    [Header("Manual Quest Management")] // Inspector UI Section
    public string questToAdd;
    public string questToRemove;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("QuestManager: Duplicate detected, destroying this instance.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("QuestManager: Initialized and set to persist.");

        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadQuests();
        LoadCurrentQuest();
        FindQuestTextUI();
        UpdateQuestUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"QuestManager: Scene Loaded: {scene.name}");
        FindQuestTextUI();
        UpdateQuestUI();
    }

    private void FindQuestTextUI()
    {
        GameObject questTextObject = GameObject.Find("Text");

        if (questTextObject != null)
        {
            questTextUI = questTextObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("QuestManager: Could not find 'Text' GameObject in scene.");
        }
    }

    private void LoadQuests()
    {
        if (PlayerPrefs.HasKey(QUESTS_KEY))
        {
            string savedQuests = PlayerPrefs.GetString(QUESTS_KEY);
            activeQuests = new List<string>(savedQuests.Split('|'));
            Debug.Log($"✅ QuestManager: Loaded Quests - {savedQuests}");
        }
        else
        {
            Debug.LogWarning("⚠️ QuestManager: No saved quests found in PlayerPrefs.");
        }
    }

    private void LoadCurrentQuest()
    {
        currentQuest = PlayerPrefs.GetString(CURRENT_QUEST_KEY, "");
        Debug.Log("QuestManager: Current Quest Loaded - " + currentQuest);
    }

    private void SaveQuests()
    {
        string questsData = string.Join("|", activeQuests);
        PlayerPrefs.SetString(QUESTS_KEY, questsData);
        PlayerPrefs.SetString(CURRENT_QUEST_KEY, currentQuest);
        PlayerPrefs.Save();
        Debug.Log("QuestManager: Quests Saved.");
    }

    private void UpdateQuestUI()
    {
        if (questTextUI != null)
        {
            questTextUI.text = activeQuests.Count > 0 ? string.Join("\n", activeQuests) : "No Active Quests";
        }
        else
        {
            Debug.LogWarning("QuestManager: questTextUI is not assigned. Will try to find it next scene load.");
        }
    }

    public void AddQuest(string quest)
    {
        if (!string.IsNullOrEmpty(quest) && !activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            SaveQuests();
            UpdateQuestUI();
            Debug.Log($"✅ QuestManager: Quest '{quest}' added.");
        }
    }

    public void SetCurrentQuest(string quest)
    {
        currentQuest = quest;
        SaveQuests();
        UpdateQuestUI();
        Debug.Log($"✅ QuestManager: Current quest set to '{quest}'.");
    }

    public void CompleteQuest(string quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
        }

        if (currentQuest == quest)
        {
            currentQuest = "";
        }

        SaveQuests();
        UpdateQuestUI();
        Debug.Log($"✅ QuestManager: Quest '{quest}' completed and removed.");
    }

    public void ReplaceQuest(string oldQuest, string newQuest)
    {
        CompleteQuest(oldQuest);
        AddQuest(newQuest);
        SetCurrentQuest(newQuest);
    }

    // === NEW METHODS FOR INSPECTOR BUTTONS ===
    
    public void AddQuestFromInspector()
{
    if (!string.IsNullOrEmpty(questToAdd))
    {
        AddQuest(questToAdd);
        SetCurrentQuest(questToAdd); // <-- this line ensures it becomes the current quest
        questToAdd = ""; // Clear the field after adding
    }
}

    public void RemoveQuestFromInspector()
    {
        if (!string.IsNullOrEmpty(questToRemove))
        {
            CompleteQuest(questToRemove);
            questToRemove = ""; // Clear the field after removing
        }
    }

    public void ClearAllQuests()
    {
        activeQuests.Clear();
        currentQuest = "";
        SaveQuests();
        UpdateQuestUI();
        Debug.Log("✅ QuestManager: All quests cleared.");
    }
}