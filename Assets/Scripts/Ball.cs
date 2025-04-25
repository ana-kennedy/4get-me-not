using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;

    private bool isDragging = false;
    private Vector2 dragStartPos;
    public Animator GolfUI;
    public AudioSource audioSource;
    public AudioClip clip1; // Launch sound
    public AudioClip clip2; // Movement sound
    public AudioClip clip3; // Not used yet

    private bool isPlayingMovementSound = false;

    [System.Obsolete]
    void Update()
    {
        PlayerInput();
        HandleMovementSound();

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
    }

[System.Obsolete]
private void PlayerInput()
{
    Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // Increase the selectable area without affecting physics colliders
    float selectionRadius = 1.5f; // Adjust this for a bigger click area

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
    Vector2 dragDir = dragStartPos - pos; // Direction from start
    float dragDistance = dragDir.magnitude;

    // Use a non-linear curve for more control at short drags
    float adjustedPower = Mathf.Pow(dragDistance, 1.5f) * power; 

    lr.SetPosition(0, transform.position);
    lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude(dragDir.normalized * adjustedPower, maxPower));
}

    [System.Obsolete]
    private void DragRelease(Vector2 pos)
{
    audioSource.PlayOneShot(clip1, 0.3f); // Play shot sound

    isDragging = false;
    lr.positionCount = 0;

    Vector2 dragDir = dragStartPos - pos; // Determine release direction
    float dragDistance = dragDir.magnitude;

    if (dragDistance < 0.3f) return; // Avoid accidental tiny shots

    // Use a non-linear scaling function for more fine-tuned control
    float adjustedPower = Mathf.Pow(dragDistance, 1.5f) * power;

    rb.velocity = Vector2.ClampMagnitude(dragDir.normalized * adjustedPower, maxPower);
}

    [System.Obsolete]
    private void HandleMovementSound()
    {
        if (rb.velocity.magnitude > 3f) // Ball is moving
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
        else // Ball has stopped
        {
            if (isPlayingMovementSound)
            {
                audioSource.Stop();
                isPlayingMovementSound = false;
            }
        }
    }
}