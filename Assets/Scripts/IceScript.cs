using UnityEngine;

public class IceScript : MonoBehaviour
{
    public bool IsOnIce = false;
    public float iceDrag = 0.05f; // Low drag for smooth sliding
    public float iceAcceleration = 1.5f; // Gradual force application on movement

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            IsOnIce = true;
            MakeBallSlippery(other.GetComponent<Rigidbody2D>());
        }
    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            IsOnIce = false;
            RestoreBallFriction(other.GetComponent<Rigidbody2D>());
        }
    }

    [System.Obsolete]
    private void MakeBallSlippery(Rigidbody2D rb)
    {
        if (rb == null) return;

        rb.drag = iceDrag; // Low friction for smooth gliding
        rb.angularDrag = 0; // Allow free rotation

        // Gradually apply a force based on the current movement direction
        Vector2 forceDirection = rb.velocity.normalized;
        rb.AddForce(forceDirection * iceAcceleration, ForceMode2D.Force);
    }

    [System.Obsolete]
    private void RestoreBallFriction(Rigidbody2D rb)
    {
        if (rb == null) return;

        rb.drag = 1.5f; // Restore normal friction to stop the sliding
        rb.angularDrag = 1; // Restore normal rotation behavior
    }
}