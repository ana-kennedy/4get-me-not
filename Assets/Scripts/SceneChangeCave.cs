using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeCave : MonoBehaviour
{
    public bool PlayerIsClose;
    public AudioSource audioSource;
    public AudioClip clip;
    public String LevelName;
    public Animator Transition;
    public GameObject TransitionObj;
    public GameObject MenuUI;
    public GameObject QuestsUI;
    public GameObject QuestsUITxt;

    void Update()
    {
    // Scene Load Trigger
    if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.Instance.SavePlayerPosition(SceneManager.GetActiveScene().name, player.transform.position);
        }
        else
        {
            Debug.LogWarning("SavePlayerPosition: Player not found in the scene!");
        }
        
        audioSource.PlayOneShot(clip, 0.5f);
        TransitionObj.SetActive(true);
        MenuUI.SetActive(false);
        QuestsUI.SetActive(false);
        QuestsUITxt.SetActive(false);
        Transition.SetBool("Active", true);
        StartCoroutine(NextLevel()); 
    }
    }

    // Scene Loading
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(LevelName);
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