using System.Collections;
using UnityEngine;

public class BigfootIceAttack : MonoBehaviour
{
    public GameObject icePrefab; // Assign the Ice prefab in the Inspector
    public Transform iceSpawnPoint; // Assign a child object as the spawn position
    public Transform ball; // Assign the Ball (Player) GameObject in the Inspector
    public float throwForce = 10f; // Adjust the force for throwing ice
    public float cooldownTime = 3f; // Time between ice throws

    private bool canThrow = true;

    [System.Obsolete]
    private void Update()
    {
        if (canThrow)
        {
            StartCoroutine(ThrowIce());
        }
    }

    [System.Obsolete]
    private IEnumerator ThrowIce()
    {
        canThrow = false;

        // Instantiate Ice at spawn point
        GameObject ice = Instantiate(icePrefab, iceSpawnPoint.position, Quaternion.identity);
        Rigidbody2D iceRb = ice.GetComponent<Rigidbody2D>();

        if (iceRb != null && ball != null)
        {
            // Calculate direction to the Ball
            Vector2 direction = (ball.position - iceSpawnPoint.position).normalized;
            iceRb.velocity = direction * throwForce;
        }

        yield return new WaitForSeconds(cooldownTime);
        canThrow = true;
    }
}