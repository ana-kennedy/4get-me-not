using UnityEngine;

public class MicroCheck : MonoBehaviour
{
public GameObject MicroHotbar;
    void Start()
    {
        if(GameManager.Instance.GetEventState("unlockedMicro"))
        {
            MicroHotbar.SetActive(true);
        }
        else 
        {
            MicroHotbar.SetActive(false);
        }
    }


}
