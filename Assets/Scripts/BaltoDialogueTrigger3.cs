using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaltoDialogueTrigger3 : MonoBehaviour
{
    #region Public References
    public GameObject DialogueBox;
    public GameObject AnastasiaDialogueBox;
    public GameObject MamaGnome;
    public GameObject MamaGnomeCutscene;
    public GameObject Anastasia;
    public Animator AnastasiaAni;
    public GameObject Camera;
    public Animator MamaGnomeCutsceneAni;
    public AudioSource audioSource;
    public AudioClip interactFX;
    public AudioClip explosionFX;
    public GameObject TeleportParticle;
    public GameObject Soundtrack2;
    public GameObject Soundtrack3;
    public MamaGnomeDialogueUI4 mamaGnomeDialogue;
    public Light2D explosionLight;
    public MonoBehaviour baltoRideComponent; // New field added
    public Rigidbody2D baltoRigidbody; // New public reference added
    #endregion

    #region Private Variables
    private bool hasTriggered = false;
    private bool lightTriggered = false;
    private bool SpawnSequence = false;
    #endregion

    #region Unity Methods

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balto") && !hasTriggered)
        {
            hasTriggered = true;

            GameObject balto = GameObject.FindGameObjectWithTag("Balto");
            if (balto != null)
            {
                if (baltoRideComponent != null)
                {
                    var type = baltoRideComponent.GetType();
                    var field = type.GetField("forwardSpeed");
                    if (field != null && field.FieldType == typeof(float))
                    {
                        field.SetValue(baltoRideComponent, 0f);
                    }
                }

                if (baltoRigidbody != null)
                {
                    baltoRigidbody.velocity = Vector2.zero;
                    baltoRigidbody.bodyType = RigidbodyType2D.Static;
                }
            }

            DialogueBox.SetActive(true);
            MamaGnome.SetActive(false);
            Soundtrack2.SetActive(false);
            MamaGnomeCutscene.SetActive(true);
            MamaGnomeCutsceneAni.SetBool("Active", true);
            audioSource.PlayOneShot(interactFX, 0.5f);

            if (Camera != null)
            {
                CameraFocusObjectZoomed2 cameraFocus1 = Camera.GetComponent<CameraFocusObjectZoomed2>();
                CameraFocusObjectZoomed3 cameraFocus2 = Camera.GetComponent<CameraFocusObjectZoomed3>();
                cameraFocus2.enabled = true;
                cameraFocus1.enabled = false;
            }
        }
    }

    void Update()
    {
        if (mamaGnomeDialogue != null && mamaGnomeDialogue.isFinished && !lightTriggered)
        {
            lightTriggered = true;

            MamaGnomeCutscene.SetActive(false);
            AnastasiaAni.SetBool("Active", true);
            TeleportParticle.SetActive(true);
            audioSource.PlayOneShot(explosionFX, 0.7f);
            StartCoroutine(FXParticleOff(1f));
            StartCoroutine(LightFlash());
            StartCoroutine(AnastasiaScene(1f));
        }

        if (mamaGnomeDialogue != null && mamaGnomeDialogue.spawnAnastasia && !SpawnSequence)
        {
            SpawnSequence = true;

            Anastasia.SetActive(true);
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator FXParticleOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        TeleportParticle.SetActive(false);
    }

    private IEnumerator LightFlash()
    {
        if (explosionLight != null)
        {
            explosionLight.intensity = 10f;
            yield return new WaitForSeconds(2f);

            float duration = 1f;
            float elapsed = 0f;
            float startIntensity = explosionLight.intensity;
            float endIntensity = 1f;

            while (elapsed < duration)
            {
                explosionLight.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            explosionLight.intensity = endIntensity;
        }
    }

    private IEnumerator AnastasiaScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(Camera != null)
        {
            CameraFocusObjectZoomed3 cameraFocus2 = Camera.GetComponent<CameraFocusObjectZoomed3>();
            CameraFocusObject8 cameraFocus3 = Camera.GetComponent<CameraFocusObject8>();

            cameraFocus2.enabled = false;
            cameraFocus3.enabled = true;
            Soundtrack3.SetActive(true);
            AnastasiaDialogueBox.SetActive(true);
        }
    }
    #endregion
}
