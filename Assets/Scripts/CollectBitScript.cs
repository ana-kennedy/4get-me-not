using System.Collections;
using TMPro;
using UnityEngine;

public class CollectBitScript : MonoBehaviour
{

    public TextMeshProUGUI BitCountText;
    public GameObject bit;
    public GameObject bitUI;
    public Animator bitUIAni;
    public AudioSource audioSource;
    public AudioClip clip1;
    public string customBitID;

    private bool hasScored = false; // Prevents multiple score triggers
    private string bitID; // Unique identifier for this bit

    private void Start()
    {
        // Generate a unique ID for this bit based on its scene and customBitID
        bitID = $"{gameObject.scene.name}_Bit_{customBitID}";

        // If this bit was already collected, hide it
        if (GameManager.Instance.IsBitCollected(bitID))
        {
            bit.SetActive(false);
            gameObject.SetActive(false); // Fully disable the bit object
        }
        
        UpdateBitUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasScored && other.CompareTag("Ball")) // Ensures scoring happens once
        {
            hasScored = true; // Mark as triggered

            audioSource.PlayOneShot(clip1, 0.5f);
            bitUI.SetActive(true);
            bitUIAni.SetBool("Active", true);
            StartCoroutine(PopupUI(1f));

            GameManager.Instance.AddBit(); // Call GameManager to track bit count
            GameManager.Instance.MarkBitAsCollected(bitID); // Save bit collection
            UpdateBitUI();
        }
    }

    private void UpdateBitUI()
    {
        int currentBits = GameManager.Instance.GetBitCount();
        BitCountText.text = currentBits.ToString();
    }

    private IEnumerator PopupUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        bitUI.SetActive(false);
        bitUIAni.SetBool("Active", false);
        bit.SetActive(false);
        gameObject.SetActive(false); // Fully disable the bit object
    }
}