using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewMenuExitSave : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
public GameObject ExitSaveHUD;
public AudioSource audioSource;
public AudioClip selectFX;
public AudioClip hoverFX;
public Image image;
private Color defaultColor;
public bool onOff;
private CanvasGroup canvasGroup;
private void EnsureCanvasGroup()
{
    if (canvasGroup == null)
    {
        canvasGroup = ExitSaveHUD.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = ExitSaveHUD.AddComponent<CanvasGroup>();
    }
}

     private void Awake()
    {
        if (image != null)
        {
            defaultColor = image.color;
            image.color = defaultColor;
        }
    }

    void Update()
    {
        if (ExitSaveHUD != null)
        {
            if (onOff && !ExitSaveHUD.activeSelf)
            {
                EnsureCanvasGroup();

                ExitSaveHUD.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(FadeIn());
            }
            else if (!onOff && ExitSaveHUD.activeSelf)
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
    if (onOff) return; // Prevent re-triggering when already active
    onOff = true;
    audioSource.PlayOneShot(selectFX, 2f);
    Debug.Log("hasClicked ExitSave");
    
    EnsureCanvasGroup();
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

    public void HoverEnter()
    {
        OnPointerEnter(new PointerEventData(EventSystem.current));
    }

    public void HoverExit()
    {
        OnPointerExit(new PointerEventData(EventSystem.current));
    }

      private IEnumerator FadeIn()
    {
        EnsureCanvasGroup();

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

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public IEnumerator FadeOut()
    {
        EnsureCanvasGroup();

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

        if (ExitSaveHUD != null)
            ExitSaveHUD.SetActive(false);

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

    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }

}
