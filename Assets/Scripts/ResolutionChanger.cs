using UnityEngine;
using UnityEngine.EventSystems;

public class ResolutionChanger : MonoBehaviour
{
    public enum ResolutionOption { HD_1920x1080, QHD_1440x2560 }
    public ResolutionOption targetResolution;
    public AudioSource audioSource;
    public AudioClip selectFX;

    public void OnPointerClick(PointerEventData eventData)
    {

        audioSource.PlayOneShot(selectFX, 2f);

        switch (targetResolution)
        {
            case ResolutionOption.HD_1920x1080:
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                break;

            case ResolutionOption.QHD_1440x2560:
                Screen.SetResolution(1440, 2560, FullScreenMode.FullScreenWindow);
                break;
        }

        Debug.Log("Resolution changed to: " + targetResolution);
    }

    // Optional helper for EventTrigger UI
    public void ManualClick()
    {
        OnPointerClick(new PointerEventData(EventSystem.current));
    }
}