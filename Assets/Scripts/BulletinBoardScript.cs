using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BulletinBoardScript : MonoBehaviour

{

    public Animator InteractButton;
    public AudioSource source;
    public AudioClip clip;
    public bool PlayerIsClose;
    public GameObject Map;
    public bool MapSwitch = false;
    private CanvasGroup mapGroup;

    void Start()
    {
        mapGroup = Map.GetComponent<CanvasGroup>();
        if (mapGroup == null)
        {
            mapGroup = Map.AddComponent<CanvasGroup>();
        }
        mapGroup.alpha = 0f;
        mapGroup.interactable = false;
        mapGroup.blocksRaycasts = false;
        Map.SetActive(false);
        InteractButton.SetBool("Default", true);   
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
            MapSwitch = !MapSwitch;

            if (MapSwitch)
            {
                Map.SetActive(true);
                StartCoroutine(FadeIn());
            }
            else
            {
                StartCoroutine(FadeOut());
            }

            source.PlayOneShot(clip, 2f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
            PlayerIsClose = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetBool("Exit", true);
            InteractButton.SetBool("Enter", false);
            PlayerIsClose = false;
            Map.SetActive(false);
        }
    }

    private IEnumerator FadeIn()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mapGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        mapGroup.alpha = 1f;
        mapGroup.interactable = true;
        mapGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        float startAlpha = mapGroup.alpha;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mapGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }
        mapGroup.alpha = 0f;
        mapGroup.interactable = false;
        mapGroup.blocksRaycasts = false;
        Map.SetActive(false);
    }
}