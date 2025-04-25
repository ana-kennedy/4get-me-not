using UnityEngine;

public class GolfCoFirstEnterCheck : MonoBehaviour
{

    void Start()
    {
        if(!GameManager.Instance.GetEventState("GolfCoFirstEnter"))
        {
            GameManager.Instance.SetEventState("GolfCoFirstEnter", true);
        }
    }

  
}
