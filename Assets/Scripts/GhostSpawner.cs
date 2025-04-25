using System.Collections;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab; // Assign your Ghost prefab in the Inspector
    public GameObject ball; // Assign the Ball GameObject in the Inspector
    public float spawnRadius = 3f; // Distance from Ball where ghosts spawn
    public float ghostLifetime = 7f; // How long ghosts last
    public float spawnInterval = 7f; // Time between spawns

    private void Start()
    {
        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnGhosts()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            for (int i = 0; i < 2; i++) // Spawn two ghosts
            {
                Vector2 spawnPos = (Vector2)ball.transform.position + Random.insideUnitCircle * spawnRadius;
                GameObject ghost = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);

                GhostAI ghostAI = ghost.GetComponent<GhostAI>();
                if (ghostAI != null)
                {
                    ghostAI.SetTarget(ball.transform);
                }

                Destroy(ghost, ghostLifetime); // Destroy after 7 seconds
            }
        }
    }
}