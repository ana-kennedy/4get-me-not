using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewMenuReturn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            audioSource.PlayOneShot(selectFX, 2f);
            Debug.Log("hasClicked Return");
            StartCoroutine(ShutOff(0.1f));
        }
    }
    public GameObject HUD;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;
    public Image image;
    private Color defaultColor;

    private void Awake()
    {
        if (image != null)
        {
            defaultColor = image.color;
            image.color = defaultColor;
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
        audioSource.PlayOneShot(selectFX, 2f);
        Debug.Log("hasClicked Return");
        StartCoroutine(ShutOff(0.1f));
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

    private IEnumerator ShutOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        HUD.SetActive(false);
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

        Color finalColor = defaultColor;
        finalColor.b = targetB;
        image.color = finalColor;
    }
}
