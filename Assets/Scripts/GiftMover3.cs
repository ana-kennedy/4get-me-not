using UnityEngine;
using System.Collections;

public class GiftMover3 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 1f;       // Grid step size
    public float moveSpeed = 3f;          // Movement speed
    public LayerMask wallLayer;           // LayerMask for walls
    public LayerMask giftTileLayer;       // LayerMask for valid GiftTiles

    private bool isMoving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMoving) return;

        if (collision.collider.CompareTag("Ball") && !BallDash.canDash)
        {
            // ✅ Check if we're currently on a GiftTile
            if (!IsOnGiftTile()) return;

            Vector3 direction = (transform.position - collision.transform.position).normalized;

            // Snap direction to either X or Y
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction = new Vector3(Mathf.Sign(direction.x), 0, 0);
            }
            else
            {
                direction = new Vector3(0, Mathf.Sign(direction.y), 0);
            }

            Vector3 targetPosition = transform.position + direction * moveDistance;

            if (!Physics2D.OverlapCircle(targetPosition, 0.1f, wallLayer))
            {
                StartCoroutine(MoveGift(targetPosition));
            }
        }
    }

    private bool IsOnGiftTile()
    {
        return true;
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