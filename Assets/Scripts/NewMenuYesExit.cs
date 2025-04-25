using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewMenuYesExit : MonoBehaviour
{
  public GameObject QuestsUI;
  public GameObject LoadingUI;
  public GameObject ExitHUD;
  public AudioSource audioSource;
  public AudioClip selectFX;

 public void OnPointerClick(PointerEventData pointerEventData)
    {
        LoadingUI.SetActive(true);
        QuestsUI.SetActive(false);
        audioSource.PlayOneShot(selectFX, 2f);
        
        if (ExitHUD != null)
        {
            SpriteRenderer sr = ExitHUD.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }
        }
        
        GameManager.Instance.SaveGame();
        StartCoroutine(BackToMenu(2f));
    }

    private IEnumerator BackToMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu");
    }

    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }
}
