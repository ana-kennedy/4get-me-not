using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene01End : MonoBehaviour
{
    public GameObject LoadingScreen;
    public float changeTime;

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            StartCoroutine(NextLevel());
        }
    }


    // Scene Loading
    IEnumerator NextLevel()
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("TheCaveOutside");
    }

}