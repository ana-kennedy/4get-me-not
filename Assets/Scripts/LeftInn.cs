using System;
using UnityEngine;

public class LeftInn : MonoBehaviour
{
    public bool PlayerIsClose = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
              if (!GameManager.LeavingInn)
            {
                GameManager.LeavingInn = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerIsClose = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerIsClose = false;
        }
    }


}