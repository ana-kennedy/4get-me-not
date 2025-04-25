using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour, IPointerClickHandler
{

public GameObject UI;
public AudioSource audioSource;
public AudioClip clip;

    public void OnPointerClick(PointerEventData eventData)
    {
            UI.SetActive(true);
            audioSource.PlayOneShot(clip);

    }
}
