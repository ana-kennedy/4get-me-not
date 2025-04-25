using UnityEngine;

public class HubCheck : MonoBehaviour
{
    public GameObject Map;
    public GameObject Phase1NPC;
    public GameObject Phase2NPC;
    public GameObject Phase2Scripts;

    void Start()
    {
        if(!GameManager.Instance.GetEventState("BirdieCave1"))
        {
            Map.SetActive(false);
        }
        else
        {
            Map.SetActive(true);
        }

        if(GameManager.Instance.GetEventState("BushmanDefeated"))
        {
            Phase1NPC.SetActive(false);
            Phase2NPC.SetActive(true);
            Phase2Scripts.SetActive(true);
            
        }
        if(!GameManager.Instance.GetEventState("BushmanDefeated"))
        {
            Phase1NPC.SetActive(true);
            Phase2NPC.SetActive(false);
            Phase2Scripts.SetActive(false);
        }
        if(GameManager.Instance.GetEventState("GolfCoFirstEnter"))
        {
            Phase2NPC.SetActive(false);
            Phase2Scripts.SetActive(false);
            Phase1NPC.SetActive(true);
        }
    }

}
