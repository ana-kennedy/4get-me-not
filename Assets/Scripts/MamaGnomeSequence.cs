using System.Collections;
using UnityEngine;

public class MamaGnomeSequence : MonoBehaviour
{
    [Header("Mama Gnome")]
    public GameObject MamaGnome;
    public GameObject MamaGnomeDialogueBox;
    public GameObject MamaGnomeDialogueBox2;
    public GameObject BirdieEndDialogueBox;
    public Animator MamaGnomeAni;

    [Header("UI Elements")]
    public GameObject UI1;
    public GameObject UI2;
    public GameObject UI3;
    public GameObject UI4;
    public GameObject UI5;
    public GameObject UI6;

    [Header("Misc")]
    public GameObject Camera;
    public GameObject MainLight;
    public GameObject Soundtrack;
    public bool hasTriggered = false;
    public AudioSource audioSource;
    public AudioClip explosionFX;


    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Ball") && !hasTriggered)
        {
            
            MamaGnomeDialogueBox.SetActive(true);
            MamaGnome.SetActive(true);
            hasTriggered = true;
            MainLight.SetActive(false);
            Soundtrack.SetActive(false);

            UI1.SetActive(false);
            UI2.SetActive(false);
            UI3.SetActive(false);
            UI4.SetActive(false);
            UI5.SetActive(false);
            UI6.SetActive(false);

            
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            BallDash ballDash = other.GetComponent<BallDash>();

            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero; 
                ballRb.constraints = RigidbodyConstraints2D.FreezeAll; 
                Debug.Log("Ball frozen!");

                if (ballDash != null)
                {
                    ballDash.isMovementDisabled = true; 
                }
            }
            if (Camera != null)
            {
                // Disable CameraFollowObject
                CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
                if (followScript != null)
                {
                    followScript.enabled = false;
                }

                // Enable CameraFocusObject
                CameraFocusObject4 focusScript = Camera.GetComponent<CameraFocusObject4>();
                if (focusScript != null)
                {
                    focusScript.enabled = true;
                }
            }
    }
    }

    public void PartOneFinished()
    {
        MamaGnomeDialogueBox2.SetActive(true);
        MamaGnomeAni.SetBool("Active", true);

            // Re-enable camera follow and disable focus
    if (Camera != null)
    {
        CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
        if (followScript != null)
        {
            followScript.enabled = true;
        }

        CameraFocusObject4 focusScript = Camera.GetComponent<CameraFocusObject4>();
        if (focusScript != null)
        {
            focusScript.enabled = false;
        }
    }
    }

  public void PartTwoFinished()
{
    MamaGnome.SetActive(false);
    MainLight.SetActive(true);

    UI1.SetActive(true);
    UI2.SetActive(true);
    UI3.SetActive(true);
    UI4.SetActive(true);
    UI5.SetActive(true);
    UI6.SetActive(true);
    audioSource.PlayOneShot(explosionFX, 0.5f);
    StartCoroutine(PostGnome(1f));
}

    private IEnumerator PostGnome(float delay)
    {
        yield return new WaitForSeconds(delay);
        BirdieEndDialogueBox.SetActive(true);

          // Re-enable ball movement
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
    {
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        BallDash ballDash = ball.GetComponent<BallDash>();

        if (ballRb != null)
        {
            ballRb.constraints = RigidbodyConstraints2D.None; // Remove all constraints
            ballRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Optional: freeze rotation if needed
        }

        if (ballDash != null)
        {
            ballDash.isMovementDisabled = false;
        }
    }
    }

}
