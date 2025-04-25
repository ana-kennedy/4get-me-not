using UnityEngine;

public class SectionTwoTrigger : MonoBehaviour
{

    public bool hasTriggered = false;
    public GameObject DialogueBox;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && !hasTriggered && JailKeyScript.hasTriggered)
        {
            hasTriggered = true;
            DialogueBox.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && hasTriggered)
        {
            DialogueBox.SetActive(false);
        }
    }
}
