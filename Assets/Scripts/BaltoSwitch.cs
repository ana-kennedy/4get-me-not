using System.Collections;
using UnityEngine;

public class BaltoSwitch : MonoBehaviour
{
   public GameObject InteractButton;
   public Animator InteractAni;
   public GameObject Balto;
   public GameObject BaltoRiding;
   public GameObject WallCollision;
   public GameObject DashUI;
   public GameObject MicroUI;
   public GameObject BaltoMovementSheetUI;
   public GameObject Soundtrack;
   public GameObject Soundtrack2;
   public GameObject Camera;
   public AudioSource audioSource;
   public AudioClip interactFX;
   private bool isPlayerNear = false;
   private bool isRiding = false;
   private Transform ballTransform;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball"))
        {
            InteractButton.SetActive(true);
            InteractAni.SetBool("Enter", true);
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        InteractAni.SetBool("Enter", false);
        isPlayerNear = false;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Q))
        {
            Balto.SetActive(false);
            BaltoRiding.SetActive(true);
            WallCollision.SetActive(false);
            DashUI.SetActive(false);
            MicroUI.SetActive(false);
            Soundtrack.SetActive(false);
            Soundtrack2.SetActive(true);
            InteractButton.SetActive(false);
            BaltoMovementSheetUI.SetActive(true);
            StartCoroutine(MovementSheetOff(5f));
            audioSource.PlayOneShot(interactFX, 0.5f);

            if(Camera != null)
            {
                CameraFollowObject followCamera = Camera.GetComponent<CameraFollowObject>();
                CameraFocusObjectZoomed2 zoomCamera = Camera.GetComponent<CameraFocusObjectZoomed2>();

                followCamera.enabled = false;
                zoomCamera.enabled = true;
            }

            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            if (ball != null)
            {
                ball.transform.localScale = new Vector3(1f, 1f, 1.4f);

                BallDash ballDash = ball.GetComponent<BallDash>();
                if (ballDash != null)
                {
                    ballDash.enabled = false;
                }
                
                BallMicroSize microSize = ball.GetComponent<BallMicroSize>();
                if (microSize != null)
                {
                    microSize.enabled = false;
                }

                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                if (ballRb != null)
                {
                    ballRb.bodyType = RigidbodyType2D.Static;
                }

                Collider2D ballCollider = ball.GetComponent<Collider2D>();
                if (ballCollider != null)
                {
                    ballCollider.enabled = false;
                }

                isRiding = true;
                ballTransform = ball.transform;
            }
        }

        if (isRiding == true)
        {
            ballTransform.position = BaltoRiding.transform.position;
        }
    }

    private IEnumerator MovementSheetOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        BaltoMovementSheetUI.SetActive(false);
    }
}
