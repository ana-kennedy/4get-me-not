using UnityEngine;

public class BaltoDoorPanel : MonoBehaviour
{
    public BaltoDoorScript doorScript;
    public Animator pressedPanel;
    public bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Balto") && !hasTriggered)
        {
            hasTriggered = true;
            pressedPanel.SetBool("isPressed", true);
            if (doorScript != null)
            {
                doorScript.Presses += 1;
            }
        }
    }
}
