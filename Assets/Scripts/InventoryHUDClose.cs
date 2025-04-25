using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class InventoryHUDClose : MonoBehaviour, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip selectFX;
    public GameObject InventoryHUD;
    public Image image;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = InventoryHUD.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = InventoryHUD.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn(0.3f));
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioSource.PlayOneShot(selectFX, 2f);
        StartCoroutine(ShutOff(0.3f));
    }

    private IEnumerator ShutOff(float delay)
    {
        yield return StartCoroutine(FadeOut(delay));
        InventoryHUD.SetActive(false);
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
