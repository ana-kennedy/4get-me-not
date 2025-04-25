using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallDash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Animator animator; // Animator Reference

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;

    [Header("Dash System")]
    [SerializeField] private float dashForce = 8f;
    [SerializeField] private float dashCooldown = 2f;
    public Animator DashHotbar;
    public static bool canDash = true;

    [Header("Drag System")]
    private bool isDragging = false;
    private Vector2 dragStartPos;

    [Header("Misc")]
    public Animator GolfUI;
    public GameObject DashParticle;
    public AudioSource audioSource;
    public AudioClip clip1; // Launch sound
    public AudioClip clip2; // Movement sound
    public AudioClip clip3; // Dash sound
    [HideInInspector] public bool isMovementDisabled = false;

    private bool isPlayingMovementSound = false;

    [System.Obsolete]
    void Update()
    {
        PlayerInput();
        HandleMovementSound();
        UpdateAnimationBooleans(); // New method to update animation states

        if (isDragging)
        {
            if (GolfUI != null)
            {
                GolfUI.SetBool("PlayAnimation", true);
            }
        }
        else
        {
            GolfUI.SetBool("PlayAnimation", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            Dash();
        }
    }

    [System.Obsolete]
    private void UpdateAnimationBooleans()
    {
        if (animator == null) return; // Ensure animator is assigned

        float xVelocity = rb.velocity.x;
        float yVelocity = rb.velocity.y;

        animator.SetBool("isForward", yVelocity > 0.1f);
        animator.SetBool("isBackward", yVelocity < -0.1f);
        animator.SetBool("isRight", xVelocity > 0.1f);
        animator.SetBool("isLeft", xVelocity < -0.1f);
    }

    [System.Obsolete]
    private void PlayerInput()
    {
        if (isMovementDisabled)
        {
            Debug.Log("Movement is disabled. Waiting for RevertChanges...");
            return;
        }

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float selectionRadius = 1.5f;

        if (Input.GetMouseButtonDown(0) && Vector2.Distance(transform.position, inputPos) <= selectionRadius)
        {
            DragStart(inputPos);
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            DragChange(inputPos);
        }
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragRelease(inputPos);
        }
    }

    private void DragStart(Vector2 pos)
    {
        isDragging = true;
        dragStartPos = pos;
        lr.positionCount = 2;
    }

    private void DragChange(Vector2 pos)
    {
        Vector2 dragDir = dragStartPos - pos;
        float dragDistance = dragDir.magnitude;

        float adjustedPower = Mathf.Pow(dragDistance, 1.5f) * power;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude(dragDir.normalized * adjustedPower, maxPower));
    }

    [System.Obsolete]
    private void DragRelease(Vector2 pos)
    {
        audioSource.PlayOneShot(clip1, 0.3f);

        isDragging = false;
        lr.positionCount = 0;

        Vector2 dragDir = dragStartPos - pos;
        float dragDistance = dragDir.magnitude;

        if (dragDistance < 0.3f) return;

        float adjustedPower = Mathf.Pow(dragDistance, 1.5f) * power;

        rb.velocity = Vector2.ClampMagnitude(dragDir.normalized * adjustedPower, maxPower);
    }

    [System.Obsolete]
    private void HandleMovementSound()
    {
        if (rb.velocity.magnitude > 3f)
        {
            if (!isPlayingMovementSound)
            {
                audioSource.clip = clip2;
                audioSource.loop = true;
                audioSource.volume = 0.05f;
                audioSource.Play();
                isPlayingMovementSound = true;
            }
        }
        else
        {
            if (isPlayingMovementSound)
            {
                audioSource.Stop();
                isPlayingMovementSound = false;
            }
        }
    }

    [System.Obsolete]
    private void Dash()
    {

        if (rb.velocity.magnitude > 0.1f)
        {
            rb.velocity += rb.velocity.normalized * dashForce;
            audioSource.PlayOneShot(clip3, 0.5f);
            DashParticle.SetActive(true);
            DashHotbar.SetBool("Active", true);
            DashHotbar.SetBool("Inactive", false);
            canDash = false;
            Invoke(nameof(ResetDash), dashCooldown);
            StartCoroutine(DashParticleOff(0.5f));
        }
    }

    private void ResetDash()
    {
        canDash = true;
        DashHotbar.SetBool("Inactive", true);
        DashHotbar.SetBool("Active", false);
    }

    private IEnumerator DashParticleOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        DashParticle.SetActive(false);
    }
}