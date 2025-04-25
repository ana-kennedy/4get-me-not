using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public static bool LeavingInn = false;
    private string lastScene = "";
    public Vector2 playerPosition;

    [Header("Bit Collection")]
    private int totalBits = 0;
    // Tracks collected bits by float-based ID string (e.g., "Hub_Bit_2.5")
    private HashSet<string> collectedBits = new HashSet<string>();

    // Tracks unlocked balls by string ID
    private HashSet<string> unlockedBalls = new HashSet<string>();

    [Header("Data Storage")]
    private Dictionary<string, bool> npcTriggerStates = new Dictionary<string, bool>();
    private Dictionary<string, bool> gameEvents = new Dictionary<string, bool>();
    private float shiftPressedTime = -1f;

    [Header("Scene Start Positions")]
    private readonly Dictionary<string, Vector2> sceneStartPositions = new Dictionary<string, Vector2>
    {
        { "MainMenu", new Vector2(0, 0) },
        { "Hub", new Vector2(-3.91f, -1.3f) },
        { "Pub", new Vector2(-3.97f, -1.56f) },
        { "Inn", new Vector2(-3.22f, -2.07f) },
        { "Diner", new Vector2(-0.99f, -1.71f) },
        { "TheCaveOutside", new Vector2(-2.86f, -1.69f) },
        { "TheCaveInside", new Vector2(-2.19f, -1.29f) },
        { "GWHub", new Vector2(1.2705f, -2.27f) },
        { "NHub", new Vector2(-3.91f, -1.3f)},
        { "NHotel", new Vector2(-5.28f, -2.32f)},
        { "NCityHall", new Vector2(-7.55f, -2.3f)}
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load bit collection data
            LoadBits();
            LoadCollectedBits();
            LoadUnlockedBalls();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadBits()
    {
        totalBits = PlayerPrefs.GetInt("PlayerBits", 0);
    }

    private void LoadCollectedBits()
    {
        collectedBits.Clear();
        string savedBits = PlayerPrefs.GetString("CollectedBits", "");
        if (!string.IsNullOrEmpty(savedBits))
        {
            string[] bitIDs = savedBits.Split(',');
            foreach (string id in bitIDs)
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    collectedBits.Add(id);
                }
            }
        }
    }

    public void AddBit()
    {
        totalBits++;
        PlayerPrefs.SetInt("PlayerBits", totalBits);
        PlayerPrefs.Save();
        Debug.Log($"✅ GameManager: Bit collected! Total bits: {totalBits}");
    }

    public int GetBitCount()
    {
        return totalBits;
    }

    // bitID format example: "SceneName_Bit_2.5"
    public void MarkBitAsCollected(string bitID)
    {
        if (!collectedBits.Contains(bitID))
        {
            collectedBits.Add(bitID);
            SaveCollectedBits();
            Debug.Log($"✅ GameManager: Bit {bitID} marked as collected.");
        }
    }

    // Example bitID: "Hub_Bit_2.5"
    public bool IsBitCollected(string bitID)
    {
        return collectedBits.Contains(bitID);
    }

    private void SaveCollectedBits()
    {
        string bitData = string.Join(",", collectedBits);
        PlayerPrefs.SetString("CollectedBits", bitData);
        PlayerPrefs.Save();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("GameManager: Player not found in scene!");
            return;
        }

        string sceneKey = scene.name;
        if (PlayerPrefs.HasKey(sceneKey + "_PosX") && PlayerPrefs.HasKey(sceneKey + "_PosY"))
        {
            RestorePlayerPosition(sceneKey);
        }
        else
        {
            playerPosition = sceneStartPositions.ContainsKey(sceneKey) ? sceneStartPositions[sceneKey] : Vector2.zero;
            player.transform.position = new Vector3(playerPosition.x, playerPosition.y, -6);
            SavePlayerPosition(sceneKey, playerPosition);
        }

        lastScene = scene.name;
        if (LeavingInn && scene.name == "Hub") StartCoroutine(DelayedInnCheck());
        LeavingInn = false;

        SaveGame();
    }

    private IEnumerator DelayedInnCheck()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) player.transform.position = new Vector3(18.8f, -1.34f, -6);
    }

    public void SavePlayerPosition(string sceneKey, Vector2 position)
    {
        PlayerPrefs.SetFloat(sceneKey + "_PosX", position.x);
        PlayerPrefs.SetFloat(sceneKey + "_PosY", position.y);
        PlayerPrefs.Save();
    }

    private void RestorePlayerPosition(string sceneKey)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(
                PlayerPrefs.GetFloat(sceneKey + "_PosX"),
                PlayerPrefs.GetFloat(sceneKey + "_PosY"),
                -6
            );
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("LastSavedScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_PosX", playerPosition.x);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_PosY", playerPosition.y);

        if (QuestManager.Instance != null)
        {
            PlayerPrefs.SetString("CurrentQuest", QuestManager.Instance.currentQuest);
            Debug.Log($"✅ GameManager: Saved current quest - {QuestManager.Instance.currentQuest}");
        }

        PlayerPrefs.Save();
        Debug.Log($"✅ GameManager: Game saved in {SceneManager.GetActiveScene().name}.");
    }

    public int GetTotalBits() => totalBits;

    public void ResetAllGameData()
    {
        npcTriggerStates.Clear();
        gameEvents.Clear();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("🔥 ALL GAME DATA HAS BEEN RESET! 🔥");
    }

    public void SetNpcTriggered(string npcID, bool value)
    {
        npcTriggerStates[npcID] = value;
        PlayerPrefs.SetInt(npcID, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetNpcTriggered(string npcID)
    {
        return npcTriggerStates.ContainsKey(npcID) ? npcTriggerStates[npcID] : PlayerPrefs.GetInt(npcID, 0) == 1;
    }

    public void SetEventState(string eventID, bool value)
    {
        gameEvents[eventID] = value;
        PlayerPrefs.SetInt(eventID, value ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Added Event");
    }

    public bool GetEventState(string eventID)
    {
        return gameEvents.ContainsKey(eventID) ? gameEvents[eventID] : PlayerPrefs.GetInt(eventID, 0) == 1;
    }
    // --- Ball Unlocking ---
    private void LoadUnlockedBalls()
    {
        unlockedBalls.Clear();
        string savedBalls = PlayerPrefs.GetString("UnlockedBalls", "");
        if (!string.IsNullOrEmpty(savedBalls))
        {
            string[] ballIDs = savedBalls.Split(',');
            foreach (string id in ballIDs)
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    unlockedBalls.Add(id);
                }
            }
        }
    }

    private void SaveUnlockedBalls()
    {
        string ballData = string.Join(",", unlockedBalls);
        PlayerPrefs.SetString("UnlockedBalls", ballData);
        PlayerPrefs.Save();
    }

    public void UnlockBall(string ballID)
    {
        if (!unlockedBalls.Contains(ballID))
        {
            unlockedBalls.Add(ballID);
            SaveUnlockedBalls();
            Debug.Log($"✅ GameManager: Ball {ballID} unlocked.");
        }
    }

    public bool IsBallUnlocked(string ballID)
    {
        return unlockedBalls.Contains(ballID);
    }
}