using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestMainButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject QuestMainHUD;
    private Image image;
    private Color originalColor;
    private bool isToggled = false;
    private Coroutine fadeCoroutine;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
            originalColor = image.color;
    }

    void Update() { }

    public void OnPointerEnter(PointerEventData eventData)
    {

        audioSource.PlayOneShot(hoverFX, 2f);
        if (image != null)
        {
            Color hoverColor = new Color(originalColor.r, originalColor.g, 195f / 255f, originalColor.a);
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleQuestMainHUD();
        audioSource.PlayOneShot(selectFX, 2f);
    }

    void ToggleQuestMainHUD()
    {
        if (QuestMainHUD == null) return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        bool isActive = QuestMainHUD.activeSelf;

        if (isActive)
        {
            fadeCoroutine = StartCoroutine(FadeCanvasGroup(QuestMainHUD, 1f, 0f, () => QuestMainHUD.SetActive(false)));
        }
        else
        {
            CanvasGroup canvas = QuestMainHUD.GetComponent<CanvasGroup>();
            if (canvas == null)
                canvas = QuestMainHUD.AddComponent<CanvasGroup>();
            canvas.alpha = 0f;
            QuestMainHUD.SetActive(true);
            fadeCoroutine = StartCoroutine(FadeCanvasGroup(QuestMainHUD, 0f, 1f, null));
        }

        isToggled = !isActive;
    }

    System.Collections.IEnumerator FadeCanvasGroup(GameObject obj, float start, float end, System.Action onComplete)
    {
        CanvasGroup canvas = obj.GetComponent<CanvasGroup>();
        if (canvas == null)
            canvas = obj.AddComponent<CanvasGroup>();

        float duration = 0.3f;
        float elapsed = 0f;
        canvas.alpha = start;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        canvas.alpha = end;
        onComplete?.Invoke();
    }
}
