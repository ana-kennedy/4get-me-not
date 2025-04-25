using UnityEngine;

public class MicroDetection2 : MonoBehaviour
{
public GameObject DialogueBox;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(CompareTag("Ball") && !GameManager.Instance.GetEventState("unlockedMicro"))
        {
            DialogueBox.SetActive(true);
        }
    }
}
