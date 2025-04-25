using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapHover : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{

    public GameObject NamePlate;
    public GameObject LoadingScreen;
    public GameObject MenuUI;
    public GameObject QuestsUI;
    public string LevelName;

    public void OnPointerClick(PointerEventData eventData)
    {
        LoadingScreen.SetActive(true);
        MenuUI.SetActive(false);
        QuestsUI.SetActive(false);
        NamePlate.SetActive(false);
        StartCoroutine(LoadLevel(2f));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NamePlate.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        NamePlate.SetActive(false);
    }

    private IEnumerator LoadLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(LevelName);
    }
}
