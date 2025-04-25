using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewMenuBitOptionsClose : MonoBehaviour
{
    public GameObject OptionsHUD;
    public GameObject HUD;
    public AudioSource audioSource;
    public AudioClip selectFX;

    [System.Obsolete]
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioSource.PlayOneShot(selectFX, 2f);

        // Set onOff to false to prevent OptionsHUD from being re-enabled
        NewMenuOptions optionsScript = FindObjectOfType<NewMenuOptions>();
        if (optionsScript != null)
        {
            optionsScript.onOff = false;
        }

        StartCoroutine(CloseUI(0.1f));
    }

    [System.Obsolete]
    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }
    
    private IEnumerator CloseUI(float delay)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup bitJournalCanvas = OptionsHUD.GetComponent<CanvasGroup>();
        if (bitJournalCanvas == null) bitJournalCanvas = OptionsHUD.AddComponent<CanvasGroup>();

        float duration = 0.2f;
        float elapsed = 0f;
        float startAlphaBJ = bitJournalCanvas.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            bitJournalCanvas.alpha = newAlpha;
            yield return null;
        }

        bitJournalCanvas.alpha = 0f;
        OptionsHUD.SetActive(false);
    }
}
