using UnityEngine;

public class LightFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the Player GameObject in the Inspector
    private Vector3 offset = new Vector3(0f, 0f, 0f); // Adjust the height offset as needed

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
