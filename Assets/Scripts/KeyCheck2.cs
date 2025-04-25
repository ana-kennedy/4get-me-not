using UnityEngine;

public class KeyCheck2 : MonoBehaviour
{
    private KeyScript2 keyScript2;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KeyDoor2"))
        {
            keyScript2 = FindObjectOfType<KeyScript2>(); // Find the Key script in the scene

            if (keyScript2 != null)
            {
                keyScript2.UseKey(); // Call the function to open the door
            }
        }
    }
}
