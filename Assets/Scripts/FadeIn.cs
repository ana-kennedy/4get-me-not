using UnityEngine;

public class FadeIn : MonoBehaviour
{
public GameObject FadeInUI;
    void Start()
    {
        FadeInUI.SetActive(true);
        Invoke("Deactivate", 3f);
    }

    private void Deactivate()
    {
        FadeInUI.SetActive(false);
    }


}
