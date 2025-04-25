using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingScriptMenu : MonoBehaviour
{
    [Header("Objects")]
    public GameObject LoadingScreen;

    [Header("Text Settings")]
    public TMP_Text textComponent;

    public DialogueData dialogueData; 
    public DialogueData newgameData;
    public DialogueData loadgameData;
    public DialogueData optionsData;
    public DialogueData mainMenuData;

    public float typingSpeed = 0.2f; 
    public float sentencePauseTime = 1f; 
    public bool startOnAwake = false; 
    public bool firstTextFinished = false;

    private string fullText;
    private Coroutine typingCoroutine;

    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioClip typingSound;
    public float soundPitchVariance = 0.1f;
    public float fadeOutDuration = 3.5f;

    [Header("Cursor Settings")]
    public string cursorChar = "_";
    public float blinkRate = 0.5f;

    private Coroutine cursorCoroutine;
    private bool showCursor = true;

    private enum MenuState { Main, ConfirmNewGame, ConfirmLoadGame, Options }
    private MenuState currentState = MenuState.Main;

  private void Start()
{
    if (dialogueData != null && textComponent != null)
    {
        mainMenuData = dialogueData; // 💾 Save original intro
        Invoke("StartTyping", 2f);
    }
}

    private void Update()
    {
        if (!firstTextFinished) return;

        switch (currentState)
        {
            case MenuState.Main:
                if (Input.GetKeyDown(KeyCode.Alpha1)) NewGameText();
                if (Input.GetKeyDown(KeyCode.Alpha2)) LoadGameText();
                if (Input.GetKeyDown(KeyCode.Alpha3)) OptionsText();
                break;

           case MenuState.ConfirmNewGame:
                if (Input.GetKeyDown(KeyCode.Y)) StartCoroutine(LoadNewGameScene());
                if (Input.GetKeyDown(KeyCode.N))
                {
                    dialogueData = mainMenuData; // 👈 Restore intro
                    currentState = MenuState.Main;
                    StartTyping();
                }
            break;

case MenuState.ConfirmLoadGame:
    if (Input.GetKeyDown(KeyCode.Y)) LoadLastSavedScene();
    if (Input.GetKeyDown(KeyCode.N))
    {
        dialogueData = mainMenuData; // 👈 Restore intro
        currentState = MenuState.Main;
        StartTyping();
    }
    break;

case MenuState.Options:
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        Screen.SetResolution(1024, 1080, FullScreenMode.Windowed);
        Debug.Log("Resolution set to 1024x1080");
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        Screen.SetResolution(2560, 1400, FullScreenMode.Windowed);
        Debug.Log("Resolution set to 2560x1400");
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        dialogueData = mainMenuData;
        currentState = MenuState.Main;
        StartTyping();
    }
    break;
        }
    }

public void StartTyping()
{
    if (typingCoroutine != null) StopCoroutine(typingCoroutine);
    if (cursorCoroutine != null) StopCoroutine(cursorCoroutine);

    fullText = dialogueData != null ? dialogueData.dialogueText : "";
    textComponent.text = "";
    typingCoroutine = StartCoroutine(TypeText());
}

    private IEnumerator TypeText()
    {
        firstTextFinished = false;
        textComponent.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            char letter = fullText[i];
            textComponent.text += letter;

            if (audioSource != null && typingSound != null)
            {
                audioSource.pitch = Random.Range(1f - soundPitchVariance, 1f + soundPitchVariance);
                audioSource.PlayOneShot(typingSound, 0.15f);
            }

            if (letter == '.' || letter == '!' || letter == '?')
                yield return new WaitForSeconds(sentencePauseTime);
            else
                yield return new WaitForSeconds(typingSpeed);
        }

        firstTextFinished = true;

        // Start blinking cursor after typing finishes
        if (cursorCoroutine != null) StopCoroutine(cursorCoroutine);
        cursorCoroutine = StartCoroutine(BlinkCursor());
    }

private void NewGameText()
{
    dialogueData = newgameData;
    currentState = MenuState.ConfirmNewGame;
    StartTyping();
}

private void LoadGameText()
{
    dialogueData = loadgameData;
    currentState = MenuState.ConfirmLoadGame;
    StartTyping();
}

private void OptionsText()
{
    dialogueData = optionsData;
    currentState = MenuState.Options;
    StartTyping();
}

    private IEnumerator LoadNewGameScene()
    {
        textComponent.text = "BOOTING UP NEW SESSION...";
        yield return new WaitForSeconds(1f);

        if (LoadingScreen != null)
            LoadingScreen.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Hub"); // 🎯 Replace with your first scene
    }

    private void LoadLastSavedScene()
    {
        string lastScene = PlayerPrefs.GetString("LastSavedScene", "");

        if (string.IsNullOrEmpty(lastScene) || lastScene == "MainMenu")
        {
            Debug.LogWarning("No save found, defaulting to Hub.");
            lastScene = "Hub";
        }

        SceneManager.LoadScene(lastScene);
        SceneManager.sceneLoaded += RestoreGameData;
    }

    private void RestoreGameData(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= RestoreGameData;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerPosition = new Vector2(
                PlayerPrefs.GetFloat(scene.name + "_PosX", 0),
                PlayerPrefs.GetFloat(scene.name + "_PosY", 0)
            );

            QuestManager.Instance.currentQuest = PlayerPrefs.GetString("CurrentQuest", "");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(GameManager.Instance.playerPosition.x, GameManager.Instance.playerPosition.y, -6);
            }
        }
        else
        {
            Debug.LogError("GameManager not found! Could not restore data.");
        }
    }

    private IEnumerator BlinkCursor()
{
    while (true)
    {
        showCursor = !showCursor;

        if (showCursor)
            textComponent.text = fullText + cursorChar;
        else
            textComponent.text = fullText;

        yield return new WaitForSeconds(blinkRate);
    }
}
}