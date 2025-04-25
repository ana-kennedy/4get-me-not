using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryHUDScript : MonoBehaviour
{
    public GameObject InventoryHUD;
    public GameObject Elements;
    public Image image;
    private CanvasGroup canvasGroup;
    private bool isVisible = false;
    public AudioSource audioSource;
    public AudioClip selectFX;

    //Golf Balls
    public GameObject SlimeBall;
    public GameObject SnowBall;
    public GameObject WaterBall;
    public GameObject SkyBall;
    public GameObject UltimaBall;

    void Start()
    {
        canvasGroup = image.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = image.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Elements.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            audioSource.PlayOneShot(selectFX, 2f);
            isVisible = !isVisible;

            if (isVisible)
            {
                Elements.SetActive(true);
                StartCoroutine(FadeIn());
            }
            else
            {
                StartCoroutine(FadeOut());
            }
        }

        //Inventory Logic

           if (GameManager.Instance.IsBallUnlocked("SlimeBall"))
        {
            SlimeBall.SetActive(true);
        }
        else
        {
            SlimeBall.SetActive(false);
        }

        if(GameManager.Instance.IsBallUnlocked("SnowBall"))
        {
            SnowBall.SetActive(true);
        }
        else
        {
            SnowBall.SetActive(false);
        }

        if(GameManager.Instance.IsBallUnlocked("WaterBall"))
        {
            WaterBall.SetActive(true);
        }
        else
        {
            WaterBall.SetActive(false);
        }

        if(GameManager.Instance.IsBallUnlocked("SkyBall"))
        {
            SkyBall.SetActive(true);
        }
        else
        {
            SkyBall.SetActive(false);
        }

        if(GameManager.Instance.IsBallUnlocked("UltimaBall"))
        {
            UltimaBall.SetActive(true);
        }
        else
        {
            UltimaBall.SetActive(false);
        }
    }

    private IEnumerator FadeIn()
    {
        if (InventoryHUD != null)
        {
            InventoryHUD.SetActive(true);
        }

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
        Elements.SetActive(false);

        if (InventoryHUD != null)
        {
            InventoryHUD.SetActive(false);
        }
    }
}
