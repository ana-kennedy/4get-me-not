using Unity.VisualScripting;
using UnityEngine;

public class NomeMapCheck : MonoBehaviour
{

    public GameObject NomeMapHover;

    void Update()
    {
        if(GameManager.Instance.GetEventState("GolfCoFirstEnter"))
        {
            if(GameManager.Instance.GetEventState("GolfCoFirstEnter"))
            {
                NomeMapHover.SetActive(true);
            }
            else 
            {
                NomeMapHover.SetActive(false);
            }
        }
    }
}
