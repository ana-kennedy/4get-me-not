using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestIconScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image image;
    private Color originalColor;
    private bool isClicked = false;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;
    public GameObject QuestMainButton;
    public GameObject QuestSideButton;
    public GameObject QuestCloseButton;
    public GameObject QuestLogNameHUD;
    public GameObject SideQuestHUD;
    public GameObject MainQuestHUD;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestHUD();
            ToggleIconColor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null && !isClicked)
        {
            Color hoverColor = new Color(originalColor.r, originalColor.g, 190f / 255f, originalColor.a);
            image.color = hoverColor;
            audioSource.PlayOneShot(hoverFX, 2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null && !isClicked)
        {
            image.color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleQuestHUD();
        ToggleIconColor();
    }

    void ToggleQuestHUD()
    {
        StopAllCoroutines();

        audioSource.PlayOneShot(selectFX, 2f);

        bool isActive = QuestMainButton.activeSelf;

        ToggleFade(QuestMainButton, isActive);
        ToggleFade(QuestSideButton, isActive);
        ToggleFade(QuestCloseButton, isActive);
        ToggleFade(QuestLogNameHUD, isActive);
        ToggleFade(SideQuestHUD, isActive);
        ToggleFade(MainQuestHUD, isActive);
    }

    void ToggleFade(GameObject obj, bool isActive)
    {
        if (obj == null) return;

        if (isActive)
        {
            StartCoroutine(FadeCanvasGroup(obj, 1f, 0f, () => obj.SetActive(false)));
        }
        else
        {
            CanvasGroup canvas = obj.GetComponent<CanvasGroup>();
            if (canvas == null)
                canvas = obj.AddComponent<CanvasGroup>();
            canvas.alpha = 0f;
            obj.SetActive(true);
            StartCoroutine(FadeCanvasGroup(obj, 0f, 1f, null));
        }
    }

    void ToggleIconColor()
    {
        if (image == null) return;

        if (!isClicked)
        {
            float avg = (image.color.r + image.color.g + image.color.b) / 3f;
            image.color = new Color(avg, avg, avg, image.color.a);
        }
        else
        {
            image.color = originalColor;
        }

        isClicked = !isClicked;
    }

    System.Collections.IEnumerator FadeCanvasGroup(GameObject target, float start, float end, System.Action onComplete)
    {
        CanvasGroup canvas = target.GetComponent<CanvasGroup>();
        if (canvas == null)
            canvas = target.AddComponent<CanvasGroup>();

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
