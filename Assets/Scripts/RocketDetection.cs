using System.Collections;
using UnityEngine;

public class RocketDetection : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    [Header("References")]
    public Animator Bigfoot;
    public GameObject Camera;
    public GameObject HitParticle;
    public GameObject Soundtrack;
    public GameObject HPBar;
    public GameObject RedTilePhase2;
    public GameObject RedTilePhase3;
    public Animator BossHP;
    public GameObject Key;
    public GameObject TeleportPanel;
    public GameObject GateCollision;
    public GameObject DialogueBox;
    public Animator GateTexture;
    public GameObject Ball; // Added reference for Ball

    [Header("Gameplay Variables")]
    public int amountHit = 0;
    public bool enableKey = false;
    public bool enableTeleport = false;

    private void Update()
    {
        switch (amountHit)
        {
            case 1:
                if (!enableKey)
                {
                    StartCoroutine(FlashRedThenChangeState("isSecond"));
                    enableKey = true;
                    Key.SetActive(true);
                    RedTilePhase2.SetActive(true);

                    SetCameraFocus<CameraFocusObject3>(true);
                    StartCoroutine(KeyFocus(2f));
                }
                break;

            case 2:
                if (!enableTeleport)
                {
                    StartCoroutine(FlashRedThenChangeState("isThird"));
                    enableTeleport = true;
                    TeleportPanel.SetActive(true);
                    RedTilePhase3.SetActive(true);
                    StartCoroutine(TriggerFocusObject4());
                }
                break;

            case 3:
                if (!Bigfoot.GetBool("isHit")) // Ensure we don't trigger this multiple times
                {
                    
                    StartCoroutine(FinalBossSequence());
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Rocket")) return;

        // If this is the final boss hit, go straight to FinalBossSequence
        if (amountHit == 2) // Since amountHit is incremented after RocketHit()
        {
            StartCoroutine(FinalBossSequence());
        }
        else // Otherwise, go through the normal hit sequence
        {
            Bigfoot.SetBool("isHit", true);
            audioSource.PlayOneShot(clip1, 0.5f);
            HitParticle.SetActive(true);

            StartCoroutine(RocketHit(1f));
            SetCameraFocus<CameraFocusObject2>(true);
        }
    }

    private IEnumerator RocketHit(float delay)
    {
        yield return new WaitForSeconds(delay);

        HitParticle.SetActive(false);
        Bigfoot.SetBool("isScreaming", true);
        audioSource.PlayOneShot(clip2, 0.5f);

        SetCameraFocus<CameraFocusObject2>(false);
        StartCoroutine(EndAnimation(1f));
    }

    private IEnumerator EndAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Bigfoot.SetBool("isScreaming", false);
        Bigfoot.SetBool("isHit", false);
        amountHit++;
    }

    private IEnumerator KeyFocus(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCameraFocus();
    }

    private IEnumerator TriggerFocusObject4()
    {
        SetCameraFocus<CameraFocusObject4>(true);
        yield return new WaitForSeconds(2f);
        SetCameraFocus<CameraFocusObject4>(false);
        SetCameraFollow(true);
    }

    private IEnumerator FlashRedThenChangeState(string nextState)
    {
        // Step 1: Set "FlashRed" to true
        BossHP.SetBool("FlashRed", true);

        // Step 2: Wait for 1.5 seconds
        yield return new WaitForSeconds(1.5f);

        // Step 3: Set "FlashRed" to false
        BossHP.SetBool("FlashRed", false);

        // Step 4: Set the next boss phase state
        BossHP.SetBool(nextState, true);
    }

    private IEnumerator FinalBossSequence()
    {
        Debug.Log("Starting Final Boss Sequence...");

        // Step 1: Play "isHit" animation, focus on Bigfoot, and stop the soundtrack
        Bigfoot.SetBool("isHit", true);
        audioSource.PlayOneShot(clip5, 0.3f);
        StartCoroutine(FlashRedThenChangeState("isFourth"));

        Debug.Log("Focusing on Bigfoot with CameraFocusObject2...");
        SetCameraFollow(false);
        SetCameraFocus<CameraFocusObject2>(true);
        GameManager.Instance.SetEventState("BushmanDefeated", true);
        Debug.Log("Boss Defeated event saved!");


        if (Soundtrack != null)
        {
            AudioSource soundtrackSource = Soundtrack.GetComponent<AudioSource>();
            if (soundtrackSource != null)
            {
                soundtrackSource.Stop();
            }
        }

        // Step 2: Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        audioSource.PlayOneShot(clip4, 0.5f);
        Bigfoot.SetBool("isDefeated", true);

        // Step 3: Open the Gate (Disable Collision, Play Gate Animation)
        if (GateCollision != null) GateCollision.SetActive(false);
        if (GateTexture != null) GateTexture.SetBool("Active", true);

        // Step 4: Wait for 3 seconds, then revert to Follow Camera
        yield return new WaitForSeconds(3f);
        Debug.Log("Switching back to Follow Camera...");
        SetCameraFocus<CameraFocusObject2>(false);
        SetCameraFollow(true);
        HPBar.SetActive(false);

        // Step 5: Teleport Ball to (0, -1, -2)
        if (Ball != null)
        {
            Debug.Log("Teleporting Ball to (0, -1, -2)...");
            Ball.transform.position = new Vector3(0, -1, -2);
            DialogueBox.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Ball GameObject not assigned in RocketDetection!");
        }
    }

    private void ResetCameraFocus()
    {
        SetCameraFocus<CameraFocusObject3>(false);
        SetCameraFollow(true);
    }

    private void SetCameraFocus<T>(bool enable) where T : MonoBehaviour
    {
        if (Camera == null) return;

        T focusScript = Camera.GetComponent<T>();
        CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();

        if (focusScript != null) focusScript.enabled = enable;

        if (followScript != null)
        {
            followScript.enabled = !enable;
        }
    }

    private void SetCameraFollow(bool enable)
    {
        if (Camera == null) return;

        CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
        if (followScript != null) followScript.enabled = enable;
    }
}