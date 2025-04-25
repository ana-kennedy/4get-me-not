using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewMenuExitAndSave : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;
    public GameObject LoadingUI;
    public GameObject HUD;
    public GameObject ReturnUI;
    public GameObject OptionsUI;
    public GameObject ExitAndSaveUI;
    public GameObject BitJournalUI;
    public GameObject QuestsUI;
    public GameObject RButton;
    public GameObject EButton;
    public GameObject OButton;
    public GameObject BButton;
    public Image image;
    public bool onOff = false;
    private CanvasGroup canvasGroup;
    private Color defaultColor;

    private void Start()
    {
        if (image != null)
            defaultColor = image.color;
    }

    private void Update()
    {
        if (HUD != null && HUD.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            OnPointerClick(null);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && selectFX != null)
        {
            audioSource.PlayOneShot(selectFX, 2f);
        }

        if (LoadingUI != null) LoadingUI.SetActive(true);
        if (QuestsUI != null) 
        {
            QuestsUI.SetActive(false);
            SetUIAlphaZero(ExitAndSaveUI);
            SetUIAlphaZero(ReturnUI);
            SetUIAlphaZero(OptionsUI);
            SetUIAlphaZero(BitJournalUI);
            SetUIAlphaZero(EButton);
            SetUIAlphaZero(RButton);
            SetUIAlphaZero(OButton);
            SetUIAlphaZero(BButton);
        }

        if (GameManager.Instance != null)
            GameManager.Instance.SaveGame();

        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        audioSource.PlayOneShot(hoverFX, 2f);
        if (image != null)
        {
            Color newColor = image.color;
            newColor.b = 195f / 255f;
            image.color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
            image.color = defaultColor;
    }

    private void SetUIAlphaZero(GameObject obj)
    {
        if (obj != null)
        {
            CanvasGroup cg = obj.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = obj.AddComponent<CanvasGroup>();
            }
            cg.alpha = 0f;
        }
    }
}
