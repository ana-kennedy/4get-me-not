using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class MamaGnomeDialogueUI3 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    public bool isOpen { get; private set; }
    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip;
    public AudioClip stab;
    public AudioClip explosionFX;

    public GameObject Camera;
    public GameObject MamaGnomeNPC;
    public GameObject Soundtrack;
    public GameObject ArmoredGnomeNPC1;
    public GameObject ArmoredGnomeNPC2;

    public Animator MamaGnomeAni;
    private typewriterEffect typewriterEffect;

    [System.Obsolete]
    private void Start()
    {
        typewriterEffect = GetComponent<typewriterEffect>();
        ShowDialogue(testDialogue);
        
        MamaGnomeNPC.SetActive(true);
        MamaGnomeAni.SetBool("Active", true);
        source2.PlayOneShot(stab, 0.7f);
        Soundtrack.SetActive(false);

        if (Camera != null)
        {
            // Disable CameraFollowObject
            CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
            if (followScript != null)
            {
                followScript.enabled = false;
            }

            // Enable CameraFocusObject
            CameraFocusObjectZoomed focusScript = Camera.GetComponent<CameraFocusObjectZoomed>();
            if (focusScript != null)
            {
                focusScript.enabled = true;
            }
        }

        FreezeBall();
    }

    [System.Obsolete]
    private void FreezeBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball != null)
        {
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            BallDash ballDash = ball.GetComponent<BallDash>();

            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero; // Stop all movement
                ballRb.constraints = RigidbodyConstraints2D.FreezeAll; // Completely freeze the ball
                Debug.Log("Ball frozen!");

                if (ballDash != null)
                {
                    ballDash.isMovementDisabled = true; // Disable input if using BallDash script
                    Debug.Log("Ball input disabled!");
                }
            }
        }
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;    
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

            // Dialogue FX Sound
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.PlayOneShot(clip, 0.3f);
            }
        }

        CloseDialogueBox();
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        MamaGnomeNPC.SetActive(false);
        source2.PlayOneShot(explosionFX, 0.5f);
        ArmoredGnomeNPC1.SetActive(false);
        ArmoredGnomeNPC2.SetActive(false);

        if (Camera != null)
        {
            // Disable CameraFollowObject
            CameraFollowObject followScript = Camera.GetComponent<CameraFollowObject>();
            if (followScript != null)
            {
                followScript.enabled = true;
            }

            // Enable CameraFocusObject
            CameraFocusObjectZoomed focusScript = Camera.GetComponent<CameraFocusObjectZoomed>();
            if (focusScript != null)
            {
                focusScript.enabled = false;
            }

            Camera.GetComponent<Camera>().orthographicSize = 5f;
        }


        UnfreezeBall();
    }

    private void UnfreezeBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball != null)
        {
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            BallDash ballDash = ball.GetComponent<BallDash>();

            if (ballRb != null)
            {
                ballRb.constraints = RigidbodyConstraints2D.None; // Re-enable movement
                ballRb.constraints = RigidbodyConstraints2D.FreezeRotation; // Keep the rotation frozen
                Debug.Log("Ball movement re-enabled!");

                if (ballDash != null)
                {
                    ballDash.isMovementDisabled = false; // Re-enable input
                    Debug.Log("Ball input re-enabled!");
                }
            }
        }
    }
}
