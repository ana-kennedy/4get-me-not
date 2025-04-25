using UnityEngine;
using System.Collections;

public class GiftMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 1f;       // Distance to move on each hit
    public float moveSpeed = 3f;          // How fast it moves
    private bool isMoving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMoving) return;

        if (collision.collider.CompareTag("Ball") && !BallDash.canDash)
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;

            // Decide axis based on greater absolute direction
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction = new Vector3(Mathf.Sign(direction.x), 0, 0); // Move on X axis
            }
            else
            {
                direction = new Vector3(0, Mathf.Sign(direction.y), 0); // Move on Y axis
            }

            Vector3 targetPosition = transform.position + direction * moveDistance;
            StartCoroutine(MoveGift(targetPosition));
        }
    }

    private IEnumerator MoveGift(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        float elapsed = 0f;
        float duration = Vector3.Distance(startPos, targetPosition) / moveSpeed;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}