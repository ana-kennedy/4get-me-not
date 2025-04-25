using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GolfCoShopItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float unlockCost = 50f;             // How many Bits required to unlock
    public string ballName = "GolfBall1";      // Name of the ball to unlock
    public AudioSource audioSource;
    public AudioClip successFX;
    public AudioClip rejectFX;
    public AudioClip hoverFX;
    public Image ballUIImage;
    public GameObject namePlate;

    private bool isUnlocked = false;
    private Color originalColor;
    private Color hoverColor;
    private Coroutine namePlateFadeCoroutine;

    void Start()
    {
        // If already unlocked (e.g., coming back to the shop)
        if (GameManager.Instance.IsBallUnlocked(ballName))
        {
            SetUnlockedVisuals();
            isUnlocked = true;
        }
        if (ballUIImage != null)
        {
            originalColor = ballUIImage.color;
            hoverColor = new Color(originalColor.r, originalColor.g, 190f / 255f, originalColor.a);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUnlocked) return;

        int currentBits = GameManager.Instance.GetBitCount();

        if (currentBits >= unlockCost)
        {
            GameManager.Instance.UnlockBall(ballName);
            SetUnlockedVisuals();

            if (audioSource && successFX)
                audioSource.PlayOneShot(successFX);

            isUnlocked = true;
        }
        else
        {
            if (audioSource && rejectFX)
                audioSource.PlayOneShot(rejectFX);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ballUIImage != null)
            ballUIImage.color = hoverColor;

        if (audioSource != null && hoverFX != null)
            audioSource.PlayOneShot(hoverFX, 2f);

        if (namePlateFadeCoroutine != null)
            StopCoroutine(namePlateFadeCoroutine);
        namePlateFadeCoroutine = StartCoroutine(FadeNamePlate(0f, 1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (namePlateFadeCoroutine != null)
            StopCoroutine(namePlateFadeCoroutine);
        namePlateFadeCoroutine = StartCoroutine(FadeNamePlate(1f, 0f));

        if (ballUIImage != null)
            ballUIImage.color = originalColor;
    }

    private void SetUnlockedVisuals()
    {
        if (ballUIImage != null)
            ballUIImage.color = Color.gray;
    }

    private System.Collections.IEnumerator FadeNamePlate(float startAlpha, float endAlpha)
    {
        if (namePlate == null) yield break;

        CanvasGroup canvasGroup = namePlate.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = namePlate.AddComponent<CanvasGroup>();
        }

        float duration = 0.3f;
        float elapsed = 0f;

        canvasGroup.alpha = startAlpha;
        namePlate.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (endAlpha == 0f)
        {
            namePlate.SetActive(false);
        }
    }
}