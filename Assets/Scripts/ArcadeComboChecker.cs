using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ArcadeComboChecker : MonoBehaviour
{
    [Header("Machine Settings")]
    public string machineID;

    [Header("UI, Other")]
    public GameObject BitUI;
    public GameObject Soundtrack;

    [Header("Audio")]
    public AudioSource fxSource;
    public AudioClip touchFX;
    public AudioClip comboFX;
    public AudioClip failFX;

    private static List<string> inputSequence = new List<string>();
    private static readonly string[] correctCombo = { "Middle", "Left", "Left", "Left", "Right", "Right", "Middle", "Left" };
    private bool playerInRange = false;
    public Animator InteractButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerIsInRange())
        {
            RegisterInput(machineID);
        }
    }

    private bool PlayerIsInRange()
    {
        return playerInRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractButton.SetBool("Exit", true);
            InteractButton.SetBool("Enter", false);
        }
    }

    private void PlayFX()
    {
        if (fxSource != null && touchFX != null)
        {
            fxSource.PlayOneShot(touchFX, 0.5f);
        }
    }

    private void CorrectCombo()
    {

        if (!GameManager.Instance.GetEventState("ArcadeBit"))
        {

            if (fxSource != null && comboFX != null)
            {
                fxSource.PlayOneShot(comboFX, 0.5f);
            }

            GameManager.Instance.AddBit();
            GameManager.Instance.SetEventState("ArcadeBit", true);
            
            BitUI.SetActive(true);
            Soundtrack.SetActive(false);
            StartCoroutine(BitUIOff(5f));
        }

    }

    private IEnumerator BitUIOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        BitUI.SetActive(false);
        Soundtrack.SetActive(true);

    }

    private void RegisterInput(string input)
    {
        inputSequence.Add(input);

        // Keep list size matching the correct combo
        if (inputSequence.Count > correctCombo.Length)
            inputSequence.RemoveAt(0);

        for (int i = 0; i < inputSequence.Count; i++)
        {
            if (inputSequence[i] != correctCombo[i])
            {
                if (fxSource != null && failFX != null)
                {
                    fxSource.PlayOneShot(failFX, 0.5f);
                }
                inputSequence.Clear(); // Reset on wrong input
                return;
            }
        }

        PlayFX();

        if (inputSequence.Count == correctCombo.Length)
        {
            CorrectCombo();
        }
    }

    void OnEnable()
    {
        inputSequence.Clear();
    }
}