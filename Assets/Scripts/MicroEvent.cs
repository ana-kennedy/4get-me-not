using UnityEngine;

public class MicroEvent : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball") && !GameManager.Instance.GetEventState("unlockedMicro"))
        {
            GameManager.Instance.SetEventState("unlockedMicro", true);
        }
    }
}
