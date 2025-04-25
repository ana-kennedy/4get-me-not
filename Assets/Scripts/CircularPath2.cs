using UnityEngine;

public class CircularPath2 : MonoBehaviour
{
    public Transform target; //Target Object to rotate around
    public float speed =2f; // Speed of the movement
    public float radius = 1f; // Radius of the circular path
    public float angle = 0f; // Current angle of the object

    void Update()
    {
        
        //Calculate the new position of the object using Mathf.Sin() and Mathf.Cos() functions

        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = target.position.y;

        //Update the position of the new object

        transform.position = new Vector3(x, y, -3);

        //Increment of the angle to move the object along the circular path

        angle += speed * Time.deltaTime;
    }
}
