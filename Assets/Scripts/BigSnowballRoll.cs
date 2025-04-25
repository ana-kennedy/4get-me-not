using System.Collections;
using UnityEngine;

public class BigSnowballRoll : MonoBehaviour
{
    public float speed = 2f;  // Speed of rolling
    public float waitTime = 1f; // Time to pause when stopping

    public float leftBoundary = -5f; // Leftmost stop point
    public float rightBoundary = 5f; // Rightmost stop point

    private Rigidbody2D rb;
    private Animator anim;
    private bool movingRight = true; // Starts moving right

    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(RollLoop());
    }

    [System.Obsolete]
    IEnumerator RollLoop()
    {
        while (true)
        {
            if (movingRight)
            {
                SetAnimationState(isRight: true, isLeft: false, isStopped: false);

                // Move right until reaching the right boundary
                while (transform.position.x < rightBoundary)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    yield return null;
                }
            }
            else
            {
                SetAnimationState(isRight: false, isLeft: true, isStopped: false);

                // Move left until reaching the left boundary
                while (transform.position.x > leftBoundary)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                    yield return null;
                }
            }

            // Stop at the boundary
            rb.velocity = Vector2.zero;
            SetAnimationState(isRight: false, isLeft: false, isStopped: true);
            yield return new WaitForSeconds(waitTime);

            // Change direction
            movingRight = !movingRight;
        }
    }

    void SetAnimationState(bool isRight, bool isLeft, bool isStopped)
    {
        if (anim != null)
        {
            anim.SetBool("isMovingRight", isRight);
            anim.SetBool("isMovingLeft", isLeft);
            anim.SetBool("isStopped", isStopped);
        }
    }
}