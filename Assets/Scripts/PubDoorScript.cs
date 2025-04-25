using UnityEngine;
using UnityEngine.SceneManagement;

public class PubDoorScript : MonoBehaviour
{
    public bool PlayerIsClose;

    void Update()
    {
               if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
               {
                SceneManager.LoadScene("Hub");
               }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = true;
        }
    }

         private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
        }
    }
}
