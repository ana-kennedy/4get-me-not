using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SoundOnOff : MonoBehaviour, IPointerClickHandler
{
    private bool isMuted = false;
    private bool initialized = false;
    private GameObject soundtrackObj;
    private bool isClickLocked = false;

    void Start()
    {
        soundtrackObj = GameObject.Find("Soundtrack");
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        ApplyMuteState();
        initialized = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!initialized || isClickLocked) return;

        StartCoroutine(ClickCooldown());

        isMuted = !isMuted;
        ApplyMuteState();

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Sound " + (isMuted ? "Off" : "On"));
    }

    private void ApplyMuteState()
    {
        if (soundtrackObj != null)
        {
            soundtrackObj.SetActive(!isMuted);
        }

        AudioListener.volume = isMuted ? 0f : 1f;
    }

    private IEnumerator ClickCooldown()
    {
        isClickLocked = true;
        yield return new WaitForSeconds(0.1f);
        isClickLocked = false;
    }

    public void ManualClick()
    {
        // Simulate a click for EventTrigger
        PointerEventData fakeData = new PointerEventData(EventSystem.current);
        OnPointerClick(fakeData);
    }
}
