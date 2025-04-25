using UnityEngine;

public class FailedPassingTrigger : MonoBehaviour
{

public GameObject DialogueBox;
public bool hasTriggered = false;

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && GnomeSuitDetection.hasTried && !notPassedDialogueUI.isOpen && !hasTriggered)
        {
            DialogueBox.SetActive(true);
            hasTriggered = true;
        }
    }


}
