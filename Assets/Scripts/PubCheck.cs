using UnityEngine;

public class PubCheck : MonoBehaviour
{
    public GameObject Travis;
    void Start()
    {
        if(GameManager.Instance.GetEventState("Pub1"))
        {
            Travis.SetActive(false);
        }
    }

}
