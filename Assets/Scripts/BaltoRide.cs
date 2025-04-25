using UnityEngine;

public class BaltoRide : MonoBehaviour
{
    public float forwardSpeed = 3f;
    public float horizontalSpeed = 2f;
    public float headbuttDuration = 1f;
    public float headbuttCooldown = 2f;
    private float cooldownTimer = 0f;
    public AudioClip headbuttSFX;
    private AudioSource audioSource;
    public static bool isHeadbutting = false;
    private float headbuttTimer = 0f;
    public Animator baltoAnimator;
    private Rigidbody2D rb;
    private float currentForwardSpeed = 0f;

    void Start()
    {
        transform.position = new Vector3(0.1070589f, 6.0752f, 0.08187028f);
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && !isHeadbutting && cooldownTimer <= 0f)
        {
            isHeadbutting = true;
            headbuttTimer = headbuttDuration;
            if (baltoAnimator != null)
            {
                baltoAnimator.SetBool("isDashing", true);
            }
            cooldownTimer = headbuttCooldown;
            if (audioSource != null && headbuttSFX != null)
            {
                audioSource.PlayOneShot(headbuttSFX, 0.2f);
            }
        }

        if (isHeadbutting)
        {
            headbuttTimer -= Time.deltaTime;
            if (headbuttTimer <= 0f)
            {
                isHeadbutting = false;
                if (baltoAnimator != null)
                {
                    baltoAnimator.SetBool("isDashing", false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        currentForwardSpeed = forwardSpeed;
        Vector2 move = new Vector2(horizontalInput * horizontalSpeed, currentForwardSpeed) * Time.deltaTime;
        Vector2 nextPosition = rb.position + move;

        rb.MovePosition(nextPosition);

        if (baltoAnimator != null)
        {
            if (currentForwardSpeed <= 0.1f)
            {
                baltoAnimator.SetBool("isRunning", false);
                baltoAnimator.SetBool("isDashing", false);
            }
            else if (!isHeadbutting)
            {
                baltoAnimator.SetBool("isRunning", true);
            }
        }
    }
}
