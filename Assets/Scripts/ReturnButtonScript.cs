using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnButtonScript : MonoBehaviour, IPointerClickHandler
{
public GameObject EntireUI;
public AudioSource audioSource;
public AudioClip clip;

    public void OnPointerClick(PointerEventData eventData)
    {
            EntireUI.SetActive(false);
            audioSource.PlayOneShot(clip);

    }
}
