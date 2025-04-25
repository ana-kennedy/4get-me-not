using System.Collections;
using UnityEngine;

public class BallMicroSize : MonoBehaviour
{
    [Header("Size Settings")]
    public Vector3 normalScale = new Vector3(2.28f, 2.28f, 2.28f);
    public Vector3 microScale = new Vector3(1.14f, 1.14f, 1.14f);

    [Header("Collider Settings")]
    public float normalRadius = 0.13f;
    public float microRadius = 0.075f;
    private CircleCollider2D circleCollider;

    [Header("Animation")]
    public Animator microAnimator; // Animator that will play your animation
    public string enterAnimName = "EnterMicro"; // Animation clip/state name
    public string exitAnimName = "ExitMicro";   // Animation clip/state name
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip microClip;

    [Header("Extras")]
    public static bool isMicro = false;
    public static bool isToggleLocked = false;
    public Animator MicroHotbar;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            Debug.LogWarning("CircleCollider2D not found on Ball!");
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isToggleLocked && GameManager.Instance.GetEventState("unlockedMicro"))
        {
            ToggleMicroSize();
        }
    }

    void ToggleMicroSize()
    {
        isMicro = !isMicro;
        if (audioSource != null && microClip != null)
        {
            audioSource.PlayOneShot(microClip, 0.2f);
        }

        if (isMicro)
        {
            MicroHotbar.SetBool("Active", true);
            MicroHotbar.SetBool("Inactive", false);

            transform.localScale = microScale;
            if (circleCollider != null) circleCollider.radius = microRadius;

        }
        else
        {
            MicroHotbar.SetBool("Inactive", true);
            MicroHotbar.SetBool("Active", false);

            transform.localScale = normalScale;
            if (circleCollider != null) circleCollider.radius = normalRadius;
        }
    }
}