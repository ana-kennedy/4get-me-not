using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnastasiaBookScript : MonoBehaviour
{
    public bool PlayerIsClose;
    public Animator Transition;
    public Animator InteractButton;
    public GameObject TransitionObj;
    public GameObject MenuUI;
    public GameObject QuestsUI;
    public AudioSource audioSource;
    public AudioClip clip;
    public String LevelName;


    void Update()
    {
    // Scene Load Trigger
    if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
    {
        audioSource.PlayOneShot(clip, 0.5f);
        GameManager.Instance.SetEventState("NAnastasiaBook", true);

         GameObject player = GameObject.FindGameObjectWithTag("Player");
         GameObject soundtrack = GameObject.FindGameObjectWithTag("Soundtrack");
         GameObject envfx = GameObject.FindGameObjectWithTag("EnvironmentEffects");
        if (player != null)
        {
            GameManager.Instance.SavePlayerPosition(SceneManager.GetActiveScene().name, player.transform.position);
        }
        else
        {
            Debug.LogWarning("SavePlayerPosition: Player not found in the scene!");
        }
        if (soundtrack != null)
        {
            soundtrack.SetActive(false);
        }
        if (envfx != null)
        {
            envfx.SetActive(false);
        }
        
        StartCoroutine(NextLevel()); 
    }
    }

    // Scene Loading
    IEnumerator NextLevel()
    {
        TransitionObj.SetActive(true);
        Transition.SetBool("Active", true);
        MenuUI.SetActive(false);
        QuestsUI.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(LevelName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = true;
            InteractButton.SetBool("Default", false);
            InteractButton.SetBool("Enter", true);
            InteractButton.SetBool("Exit", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
            InteractButton.SetBool("Exit", true);
            InteractButton.SetBool("Enter", false);
        }
    }
}