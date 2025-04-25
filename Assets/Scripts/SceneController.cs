using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool LoadInn;
    public bool LoadPub;
    public bool LoadDiner;
    public bool LoadLilTown;
    public GameObject Inn;
    public GameObject Pub;
    public GameObject Diner;
    public GameObject Exit;

    public void NextLevel()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.name == "Pub")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadPub = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

}
