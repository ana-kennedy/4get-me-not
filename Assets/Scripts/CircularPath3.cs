using UnityEngine;

public class CircularPath3 : MonoBehaviour
{
    public Transform target; // Target Object to rotate around
    public float speed = 2f; // Speed of the movement
    public float radius = 1f; // Radius of the circular path
    public float angle = 0f; // Current angle of the object

    public Animator animator;
    private float previousX;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        previousX = transform.position.x;
    }

    void Update()
    {
        // Calculate new position on circle
        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = target.position.y;

        transform.position = new Vector3(x, y, -3);

        // Determine direction faster
        float deltaX = x - previousX;

        if (deltaX > 0f)
        {
            animator.SetBool("isRight", true);
            animator.SetBool("isLeft", false);
        }
        else if (deltaX < 0f)
        {
            animator.SetBool("isLeft", true);
            animator.SetBool("isRight", false);
        }

        previousX = x;

        angle += speed * Time.deltaTime;
    }
}