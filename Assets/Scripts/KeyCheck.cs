using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    private KeyScript keyScript;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KeyDoor"))
        {
            keyScript = FindObjectOfType<KeyScript>(); // Find the Key script in the scene

            if (keyScript != null)
            {
                keyScript.UseKey(); // Call the function to open the door
            }
        }
    }
}
