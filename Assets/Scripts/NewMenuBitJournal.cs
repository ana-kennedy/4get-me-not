using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class NewMenuBitJournal : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject BitJournalHUD;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;
    public Image image;
    public bool onOff = false;

    private CanvasGroup canvasGroup;

    public void ManualClick()
    {
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onOff = !onOff;

        if (canvasGroup == null && BitJournalHUD != null)
        {
            canvasGroup = BitJournalHUD.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = BitJournalHUD.AddComponent<CanvasGroup>();
            }
        }

        if (onOff)
        {
            BitJournalHUD.SetActive(true);
            StartCoroutine(FadeIn());
        }
        else
        {
            StartCoroutine(FadeOut());
        }

        audioSource.PlayOneShot(selectFX, 2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            Color c = image.color;
            c.b = 190f / 255f;
            image.color = c;
        }

        if (audioSource != null && hoverFX != null)
        {
            audioSource.PlayOneShot(hoverFX, 2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            Color c = image.color;
            c.b = 1f;
            image.color = c;
        }
    }

    private IEnumerator FadeIn()
    {
        if (BitJournalHUD != null && !BitJournalHUD.activeSelf)
        {
            BitJournalHUD.SetActive(true);
        }

        float duration = 0.2f;
        float elapsed = 0f;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut()
    {
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
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        BitJournalHUD.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            onOff = !onOff;

            if (canvasGroup == null && BitJournalHUD != null)
            {
                canvasGroup = BitJournalHUD.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = BitJournalHUD.AddComponent<CanvasGroup>();
                }
            }

            if (onOff)
            {
                BitJournalHUD.SetActive(true);
                StartCoroutine(FadeIn());
            }
            else
            {
                StartCoroutine(FadeOut());
            }

            if (audioSource != null && selectFX != null)
            {
                audioSource.PlayOneShot(selectFX, 2f);
            }
        }
    }
}
