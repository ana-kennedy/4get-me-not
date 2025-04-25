using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundScript : MonoBehaviour, IPointerClickHandler
{

public AudioSource audioSource;
public AudioClip clip;
public bool audioSwitch = false;

void Update()
{
    if (audioSwitch == true)
    {
        AudioListener.volume = 0;
    }
    if (audioSwitch == false)
    {
        AudioListener.volume = 1;
    }
}

public void OnPointerClick(PointerEventData eventData)
{
    audioSource.PlayOneShot(clip);
    audioSwitch = !audioSwitch;
}

}
