using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound (if assigned)
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, 0.2f);
        }

        // Retrieve the last saved scene from PlayerPrefs
        string lastScene = PlayerPrefs.GetString("LastSavedScene", "");

        // ✅ Prevent reloading MainMenu if no save exists
        if (string.IsNullOrEmpty(lastScene) || lastScene == "MainMenu")
        {
            Debug.LogWarning("LoadGameButton: No saved game found! Loading default gameplay scene.");
            lastScene = "Hub"; // ✅ Replace "Hub" with your actual first gameplay scene
        }

        // Load the last saved scene
        SceneManager.LoadScene(lastScene);

        // Restore player data after the scene loads
        SceneManager.sceneLoaded += RestoreGameData;
    }

    private void RestoreGameData(Scene scene, LoadSceneMode mode)
    {
        // Unsubscribe to prevent multiple calls
        SceneManager.sceneLoaded -= RestoreGameData;

        if (GameManager.Instance != null)
        {
            // Restore player position (Z always -6)
            GameManager.Instance.playerPosition = new Vector2(
                PlayerPrefs.GetFloat(scene.name + "_PosX", 0),
                PlayerPrefs.GetFloat(scene.name + "_PosY", 0)
            );

            // Restore current quest
            QuestManager.Instance.currentQuest = PlayerPrefs.GetString("CurrentQuest", "");

            // Apply restored position to player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(GameManager.Instance.playerPosition.x, GameManager.Instance.playerPosition.y, -6);
            }
        }
        else
        {
            Debug.LogError("LoadGameButton: GameManager not found! Data restoration failed.");
        }
    }
}