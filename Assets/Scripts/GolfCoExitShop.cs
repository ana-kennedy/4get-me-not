using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GolfCoExitShop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject LoadingUI;
    public GameObject Menu;
    public GameObject Quests;
    public GameObject GolfCoHUD;
    public GameObject BitCountHUD;
    public GameObject NoteHUD;
    public AudioSource audioSource;
    public AudioClip selectFX;
    public AudioClip hoverFX;
    public Image uiImage;

    private Color originalColor;
    private Color hoverColor;

    void Start()
    {
        if (uiImage != null)
        {
            originalColor = uiImage.color;
            hoverColor = new Color(originalColor.r, originalColor.g, 190f / 255f, originalColor.a);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        Menu.SetActive(false);
        Quests.SetActive(false);
        GolfCoHUD.SetActive(false);
        BitCountHUD.SetActive(false);
        NoteHUD.SetActive(false);

        if (audioSource != null)
            audioSource.PlayOneShot(selectFX, 2f);

        if (LoadingUI != null)
            LoadingUI.SetActive(true);

        StartCoroutine(LoadHubAfterDelay());
    }

    private System.Collections.IEnumerator LoadHubAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Hub");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiImage != null)
            uiImage.color = hoverColor;

        if (audioSource != null)
            audioSource.PlayOneShot(hoverFX, 2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (uiImage != null)
            uiImage.color = originalColor;
    }
}
