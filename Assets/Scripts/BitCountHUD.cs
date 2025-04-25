using UnityEngine;
using TMPro;

public class BitCountHUD : MonoBehaviour
{
    public TextMeshProUGUI bitCountText;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        UpdateBitCountUI();
    }

    void Update()
    {
        UpdateBitCountUI();
    }

    void UpdateBitCountUI()
    {
        if (bitCountText != null && gameManager != null)
        {
            bitCountText.text = gameManager.GetTotalBits().ToString();
        }
    }
}
