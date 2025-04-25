using UnityEngine;

public class PressCheck : MonoBehaviour
{

    public BaltoDoorScript baltoDoorScript;
    public float x;
    public float y;
    public float z;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Balto"))
        {
            if (baltoDoorScript != null && baltoDoorScript.Presses < 3)
            {
                other.transform.position = new Vector3(x, y, z);
            }
        }
    }
}
