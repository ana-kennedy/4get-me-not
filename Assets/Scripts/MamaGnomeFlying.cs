using UnityEngine;

public class MamaGnomeFlying : MonoBehaviour
{
    public GameObject lightning1;
    public GameObject lightning2;
    public GameObject cratePrefab;
    public float hoverHeight = 8f;
    public float lightningInterval = 8f;
    public float lightningDuration = 5f;
    public Animator animator;

    private Transform balto;
    private float lightningTimer = 0f;
    private bool inLightningPhase = false;
    private float lightningPhaseTimer = 0f;
    private GameObject currentCrate;

    public AudioSource audioSource;
    public AudioClip lightningFX;
    public AudioClip laughFX;
    public AudioClip explosionFX;

    void Start()
    {
        balto = GameObject.FindGameObjectWithTag("Balto")?.transform;
        lightning1.SetActive(false);
        lightning2.SetActive(false);
        audioSource.PlayOneShot(explosionFX, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (balto != null)
        {
            Vector3 targetPos = new Vector3(balto.position.x, balto.position.y + hoverHeight, balto.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3f);
        }

        lightningTimer += Time.deltaTime;

        if (!inLightningPhase && lightningTimer >= lightningInterval)
        {
            inLightningPhase = true;
            lightningPhaseTimer = 0f;
            lightning1.SetActive(true);
            lightning2.SetActive(true);
            if (animator != null) animator.SetBool("isShooting", true);
            audioSource.PlayOneShot(lightningFX, 0.7f);
            audioSource.PlayOneShot(laughFX, 0.5f);
            StartCoroutine(SpawnCrateAfterDelay(1f));
        }

        if (inLightningPhase)
        {
            lightningPhaseTimer += Time.deltaTime;
            if (lightningPhaseTimer >= lightningDuration)
            {
                inLightningPhase = false;
                lightningTimer = 0f;
                lightning1.SetActive(false);
                lightning2.SetActive(false);
                if (animator != null) animator.SetBool("isShooting", false);
            }
        }
    }

    private System.Collections.IEnumerator SpawnCrateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentCrate != null)
        {
            Destroy(currentCrate);
        }
        currentCrate = Instantiate(cratePrefab, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
    }
}
