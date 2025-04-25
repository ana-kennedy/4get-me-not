using UnityEngine;

public class StaticValues : MonoBehaviour
{
public static bool TravisPubDialogue = false;

    void Update()
    {
        TravisPubDialogue = !TravisPubDialogue;
    }
}
