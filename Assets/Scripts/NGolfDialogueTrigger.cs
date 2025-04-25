using UnityEngine;

public class NGolfDialogueTrigger : MonoBehaviour
{

public GameObject DialogueBox;
public bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && !hasTriggered)
        {
            DialogueBox.SetActive(true);
            hasTriggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            DialogueBox.SetActive(false);
        }
    }
}
