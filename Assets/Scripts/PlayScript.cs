using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for coroutines

public class PlayScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(5f); // Waits for 5 seconds
        SceneManager.LoadScene("Hub");
    }
}