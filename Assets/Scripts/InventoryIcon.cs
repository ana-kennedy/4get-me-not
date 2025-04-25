using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject InventoryHUD;
    public GameObject Elements;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX; // New hover sound
    public Image image;

    private CanvasGroup canvasGroup;
    private bool isOpen = false;
    private Color originalColor;

    void Start()
    {
        if (InventoryHUD != null)
        {
            canvasGroup = InventoryHUD.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = InventoryHUD.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            InventoryHUD.SetActive(false);
        }

        if (image != null)
        {
            originalColor = image.color;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && selectFX != null)
        {
            audioSource.PlayOneShot(selectFX, 2f);
        }

        isOpen = !isOpen;

        if (isOpen)
        {
            InventoryHUD.SetActive(true);
            if (Elements != null) Elements.SetActive(true);
            StartCoroutine(FadeIn());
        }
        else
        {
            StartCoroutine(FadeOut());
            if (Elements != null) Elements.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            Color newColor = image.color;
            newColor.b = 190f / 255f;
            image.color = newColor;
        }

        if (audioSource != null && hoverFX != null) // Play hover sound
        {
            audioSource.PlayOneShot(hoverFX, 2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = originalColor;
        }
    }

    private IEnumerator FadeIn()
    {
        if (Elements != null) Elements.SetActive(true);
        float duration = 0.3f;
        float elapsed = 0f;
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
        float duration = 0.3f;
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
        InventoryHUD.SetActive(false);
        if (Elements != null) Elements.SetActive(false);
    }
}
