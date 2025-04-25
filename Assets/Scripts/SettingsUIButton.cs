using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsUIButton : MonoBehaviour, IPointerClickHandler
{

    public GameObject SettingsUI;
    public AudioSource audioSource;
    public AudioClip clip;

    public void OnPointerClick(PointerEventData eventData)
    {
            SettingsUI.SetActive(true);
            audioSource.PlayOneShot(clip);

    }
}
