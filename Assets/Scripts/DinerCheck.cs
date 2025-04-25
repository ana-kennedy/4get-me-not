using UnityEngine;

public class DinerCheck : MonoBehaviour
{
public GameObject Travis;
    void Start()
    {
        if(GameManager.Instance.GetEventState("TravisDiner1"))
        {
            Travis.SetActive(false);
        }
    }


}
