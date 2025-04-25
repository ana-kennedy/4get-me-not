using UnityEngine;

public class BossKeyCheck : MonoBehaviour
{
    private BossKeyScript keyScript;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KeyDoor"))
        {
            keyScript = FindObjectOfType<BossKeyScript>(); // Find the Key script in the scene

            if (keyScript != null)
            {
                keyScript.UseKey(); // Call the function to open the door
            }
        }
    }
}
