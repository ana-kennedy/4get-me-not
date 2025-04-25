using System.Collections;
using UnityEngine;

public class AnastasiaRoomCheck : MonoBehaviour
{

    public GameObject DialogueBox;
    public GameObject DialogueBox2;
    public GameObject QuestsUI;
    public GameObject MenuUI;
    public GameObject TxtFXUI;
    public Animator AnastasiaAni;
    public GameObject PostProcessing;
    public GameObject MainLight;
    public GameObject SmallLight;
    public GameObject Camera;
    public GameObject Soundtrack;
    public AudioSource audioSource;
    public AudioClip cutsceneMusic;
    public AudioClip cutsceneMusic2;
    public bool hasTriggered = false;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // && GameManager.Instance.GetEventState("NH3Completed") && !GameManager.Instance.GetEventState("ARCutscene")
        if(other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerRb != null)
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (playerMovement != null) {
                playerMovement.enabled = false;
                other.GetComponent<SpriteRenderer>().enabled = false;
            }

            GameManager.Instance.SetEventState("ARCutscene", true);
            MenuUI.SetActive(false);
            QuestsUI.SetActive(false);
            DialogueBox.SetActive(true);
            Soundtrack.SetActive(false);
            audioSource.PlayOneShot(cutsceneMusic, 0.7f);

            if (Camera != null)
            {
                CameraFollowObject cameraFollow = Camera.GetComponent<CameraFollowObject>();
                CameraFocusObject cameraFocus = Camera.GetComponent<CameraFocusObject>();

                cameraFollow.enabled = false;
                cameraFocus.enabled = true;
                Camera.GetComponent<Camera>().orthographicSize = 3f;
                StartCoroutine(EndSequenceOne(83f));
            }
        }
    }

    private IEnumerator EndSequenceOne(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(cutsceneMusic2, 0.7f);
        MainLight.SetActive(false);
        SmallLight.SetActive(true);
        SequenceTwo();
      
    }

    private void SequenceTwo()
    {
        DialogueBox2.SetActive(true);
        AnastasiaAni.SetBool("Active", true);
        PostProcessing.SetActive(true);
        StartCoroutine(EndSequenceTwo(53f));
        StartCoroutine(TxtSequence(49f));
    }

    private IEnumerator TxtSequence (float delay)
    {
        yield return new WaitForSeconds(delay);
        TxtFXUI.SetActive(true);
    }

    private IEnumerator EndSequenceTwo(float delay)
    {
        yield return new WaitForSeconds(delay);
        Soundtrack.SetActive(true);
        MenuUI.SetActive(true);
        QuestsUI.SetActive(true);
        MainLight.SetActive(true);
        SmallLight.SetActive(false);
        AnastasiaAni.SetBool("Active", false);
        AnastasiaAni.SetBool("Inactive", true);
        PostProcessing.SetActive(false);
        TxtFXUI.SetActive(false);
        
        if (Camera != null)
            {
                CameraFollowObject cameraFollow = Camera.GetComponent<CameraFollowObject>();
                CameraFocusObject cameraFocus = Camera.GetComponent<CameraFocusObject>();

                cameraFollow.enabled = true;
                Camera.GetComponent<Camera>().orthographicSize = 5f;
                cameraFocus.enabled = false;
            }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            if (playerRb != null)
                playerRb.constraints = RigidbodyConstraints2D.None;

            if (playerMovement != null) {
                playerMovement.enabled = true;
                player.GetComponent<SpriteRenderer>().enabled = true;
            }
            player.transform.position = new Vector3(-7.55f, -2.3f, -6f);
        }
    }

}
