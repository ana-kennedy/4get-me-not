using TMPro;
using UnityEngine;

public class BitCounterUI : MonoBehaviour
{
    public TextMeshProUGUI bitText; // Assign this in the Unity Inspector

    private void Start()
    {
        UpdateBitUI(); // Set initial value
    }

    private void Update()
    {
        UpdateBitUI(); // Update every frame to reflect changes in BitCount
    }

    private void UpdateBitUI()
    {
        if (GameManager.Instance != null && bitText != null)
        {
            bitText.text = $"{GameManager.Instance.GetBitCount()}";
        }
    }
}