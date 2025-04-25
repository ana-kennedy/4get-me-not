using Unity.Collections;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Balto"))
        {
            GameObject balto = GameObject.FindGameObjectWithTag("Balto");
            if (balto != null)
            {
            Vector3 baltoPos = balto.transform.position;
            balto.transform.position = new Vector3(0, baltoPos.y - 10f, baltoPos.z);
            }
        }   
    }
}
