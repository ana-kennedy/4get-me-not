using System.Collections;
using UnityEngine;

public class LevelTagScript2 : MonoBehaviour
{

    public GameObject Tag;
    public GameObject DialogueBox;

    void Start()
    {
        Tag.SetActive(true);
        StartCoroutine(DisableTag(2f));
    }



    private IEnumerator DisableTag(float delay)
    {
        yield return new WaitForSeconds(delay);
        Tag.SetActive(false);
        DialogueBox.SetActive(true);
    }
 
}
