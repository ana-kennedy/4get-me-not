using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class NewMenuNoExit : MonoBehaviour, IPointerClickHandler
{
  public GameObject ExitHUD;
  public AudioSource audioSource;
  public AudioClip selectFX;
  public NewMenuExitSave controller;

  public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioSource.PlayOneShot(selectFX, 2f);
        StartCoroutine(Exit(0.1f));
    }

    private IEnumerator Exit (float delay)
    {
        yield return new WaitForSeconds(delay);
        if (controller != null)
        {
            yield return controller.StartCoroutine(controller.FadeOut());
        }
    }

    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }
}
