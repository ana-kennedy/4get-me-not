using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewMenuOptions : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject OptionsHUD;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;
    public Image image;
    private Color defaultColor;
    public bool onOff;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (image != null)
        {
            defaultColor = image.color;
            image.color = defaultColor;
        }
    }

    private void Update()
    {
        if (OptionsHUD != null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                onOff = !onOff;
                audioSource.PlayOneShot(selectFX, 2f);
                Debug.Log("hasClicked Options (Key Press)");

                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 0f;
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }
            }

            if (onOff && !OptionsHUD.activeSelf)
            {
                if (canvasGroup == null)
                {
                    canvasGroup = OptionsHUD.GetComponent<CanvasGroup>();
                    if (canvasGroup == null)
                    {
                        canvasGroup = OptionsHUD.AddComponent<CanvasGroup>();
                    }
                }

                OptionsHUD.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(FadeIn());
            }
            else if (!onOff && OptionsHUD.activeSelf)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOut());
            }
        }
    }

    private void OnEnable()
    {
        if (image != null)
        {
            image.color = defaultColor;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onOff = !onOff;
        audioSource.PlayOneShot(selectFX, 2f);
        Debug.Log("hasClicked Options");
        
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeBlue(190f / 255f));
        }
        audioSource.PlayOneShot(hoverFX, 2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeBlue(1f));
        }
    }

    private IEnumerator FadeIn()
    {
        if (canvasGroup == null)
        {
            canvasGroup = OptionsHUD.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = OptionsHUD.AddComponent<CanvasGroup>();
            }
        }

        if (!OptionsHUD.activeSelf)
        {
            OptionsHUD.SetActive(true);
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        // Force final state in case the coroutine exits early or stalls
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.1f;
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
        OptionsHUD.SetActive(false);
        onOff = false;
    }

    private IEnumerator FadeBlue(float targetB)
    {
        float duration = 0.2f;
        float elapsed = 0f;
        Color startColor = image.color;
        float startB = startColor.b;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newB = Mathf.Lerp(startB, targetB, elapsed / duration);
            Color c = image.color;
            c.b = newB;
            image.color = c;
            yield return null;
        }

        Color finalColor = image.color;
        finalColor.b = targetB;
        image.color = finalColor;
    }
}
