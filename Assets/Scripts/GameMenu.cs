using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GameMenu : MonoBehaviour, IPointerClickHandler
{
    public GameObject HUD;
    public GameObject BitJournalHUD;
    public GameObject OptionsHUD;
    public bool OnOff = false;
    public AudioSource audioSource;
    public AudioClip clip1;

    //UI Elements To Fade

    public GameObject ReturnUI;
    public GameObject OptionsUI;
    public GameObject BitJournalUI;
    public GameObject SaveAndExitUI;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        // Toggle OnOff
        OnOff = !OnOff;

        // Play sound
        audioSource.PlayOneShot(clip1, 2f);

        // Directly set UI active/inactive
        if (OnOff)
        {
            StartCoroutine(FadeInUI(HUD));
            StartCoroutine(FadeInUI(ReturnUI));
            StartCoroutine(FadeInUI(OptionsUI));
            StartCoroutine(FadeInUI(BitJournalUI));
            StartCoroutine(FadeInUI(SaveAndExitUI));
        }
        else
        {
            StartCoroutine(FadeOutUI(HUD));
            StartCoroutine(FadeOutUI(ReturnUI));
            StartCoroutine(FadeOutUI(OptionsUI));
            StartCoroutine(FadeOutUI(BitJournalUI));
            StartCoroutine(FadeOutUI(SaveAndExitUI));
            if (OptionsHUD.activeSelf)
            {
                StartCoroutine(FadeOutUI(OptionsHUD));
            }
        }

        // Only turn off BitJournalHUD if HUD is being disabled
        if (!OnOff && BitJournalHUD.activeSelf)
        {
            BitJournalHUD.SetActive(false);
        }
        if (OnOff && OptionsHUD.activeSelf)
        {
            OptionsHUD.SetActive(false);
        }
    }

    private IEnumerator FadeInUI(GameObject uiElement)
    {
        CanvasGroup canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiElement.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        uiElement.SetActive(true);

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutUI(GameObject uiElement)
    {
        CanvasGroup canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiElement.AddComponent<CanvasGroup>();
        }

        float duration = 0.2f;
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        uiElement.SetActive(false);
    }
}