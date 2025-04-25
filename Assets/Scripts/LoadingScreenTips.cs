using UnityEngine;
using TMPro;

public class LoadingScreenTips : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tipText;
    
    // Array of tips to be randomly selected
    private string[] tips = 
    {
        "THIS GAME AUTOSAVES!",
        "BIRDIE KNOWS BEST, LISTEN TO HER!",
        "ALWAYS REMEMBER TO TAKE BREAKS!",
        "HAVING TROUBLE? GET GOOD!",
        "SPEND YOUR BITS AT GOLFCO IN LIL TOWN!",
        "WHO IS ANASTASIA ANYWAY?",
        "DEVELOPED BY PETALCO!",
        "WOW, YOU'RE DOING GREAT!"
    };

    private void Awake()
    {
        if (tipText == null)
        {
            Debug.LogError("LoadingScreenTips: No TextMeshProUGUI assigned! Assign one in the Inspector.");
            return;
        }

        // Pick a random tip and display it
        string randomTip = tips[Random.Range(0, tips.Length)];
        tipText.text = randomTip;
    }
}