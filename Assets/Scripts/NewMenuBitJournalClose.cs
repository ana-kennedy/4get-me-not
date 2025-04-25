using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewMenuBitJournalClose : MonoBehaviour
{
    public GameObject BitJournalUI;
    public GameObject HUD;
    public AudioSource audioSource;
    public AudioClip interactFX;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioSource.PlayOneShot(interactFX, 2f);
        StartCoroutine(CloseUI(0.1f));
    }

    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }
    
    private IEnumerator CloseUI(float delay)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup bitJournalCanvas = BitJournalUI.GetComponent<CanvasGroup>();
        if (bitJournalCanvas == null) bitJournalCanvas = BitJournalUI.AddComponent<CanvasGroup>();

        CanvasGroup hudCanvas = HUD.GetComponent<CanvasGroup>();
        if (hudCanvas == null) hudCanvas = HUD.AddComponent<CanvasGroup>();

        float duration = 0.2f;
        float elapsed = 0f;
        float startAlphaBJ = bitJournalCanvas.alpha;
        float startAlphaHUD = hudCanvas.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            bitJournalCanvas.alpha = newAlpha;
            hudCanvas.alpha = newAlpha;
            yield return null;
        }

        bitJournalCanvas.alpha = 0f;
        hudCanvas.alpha = 0f;

        BitJournalUI.SetActive(false);
        HUD.SetActive(false);
    }
}
