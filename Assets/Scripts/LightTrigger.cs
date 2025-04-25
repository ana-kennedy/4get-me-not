using UnityEngine;

public class LightTrigger : MonoBehaviour
{
public GameObject GlobalLight;
public GameObject SpotLight;
public Camera MainCamera;
public Color DefaultColor = Color.white;
public Color TriggerColor = Color.black;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball"))
        {
            GlobalLight.SetActive(false);
            SpotLight.SetActive(true);
            Camera.main.backgroundColor = Color.black;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ball"))
        {
            GlobalLight.SetActive(true);
            SpotLight.SetActive(false);
            Camera.main.backgroundColor = Color.white;
        }
    }


}
