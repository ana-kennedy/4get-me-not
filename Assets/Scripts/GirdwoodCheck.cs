using UnityEngine;

public class GirdwoodCheck : MonoBehaviour
{
    public GameObject BirdieGirdwood1;
    public GameObject BirdieTriggerGirdwood1;
    public GameObject BirdieGirdwood2;
    public GameObject BirdieTriggerGirdwood2;

    void Start()
    {
        if (GameManager.Instance.GetEventState("BushmanDefeated"))
        {
            BirdieGirdwood1.SetActive(false);
            BirdieTriggerGirdwood1.SetActive(false);
            BirdieGirdwood2.SetActive(true);
            BirdieTriggerGirdwood2.SetActive(true);
        }
    }


}
