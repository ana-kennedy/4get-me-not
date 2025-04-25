using UnityEngine;
using UnityEngine.EventSystems;

public class NoButton : MonoBehaviour, IPointerClickHandler
{
public GameObject MenuUI;
public AudioSource audioSource;
public AudioClip clip;

 public void OnPointerClick(PointerEventData pointerEventData)
 {
    MenuUI.SetActive(false);
    audioSource.PlayOneShot(clip, 0.2f);
 }
}
