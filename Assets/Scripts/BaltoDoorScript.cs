using UnityEngine;

public class BaltoDoorScript : MonoBehaviour
{
    public GameObject BaltoDoor;
    public int Presses = 0;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip activationFX;
    public bool hasTriggered = false;
    public AudioClip[] dingFX;
    private int lastPressCount = 0;

    void Update()
    {
        if (Presses > 2)
        {
            BaltoDoor.SetActive(false);
            if(!hasTriggered)
            {
            ActivateSound();
            }
            return;
        }

        if (Presses >= 1 && Presses <= 3 && Presses != lastPressCount)
        {
            int index = Presses - 1;
            if (index < dingFX.Length && dingFX[index] != null)
            {
                audioSource.PlayOneShot(dingFX[index], 0.7f);
            }
            lastPressCount = Presses;
        }
    }

    void ActivateSound()
    {
        audioSource2.PlayOneShot(activationFX, 0.7f);
        hasTriggered = true;
    }
}
