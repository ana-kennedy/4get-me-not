using System.Collections;
using UnityEngine;

public class TreeMechanic : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject applePrefab;
    public GameObject ball;
    public float minSpawnDistance = 3f;
    public float maxSpawnDistance = 6f;
    public float spawnInterval = 5f;

    private GameObject currentTree1;
    private GameObject currentTree2;
    private bool isActive = false; 
    private Coroutine spawnCoroutine; 

    private void Awake()
    {
        isActive = false;
        Debug.Log("TreeMechanic is INACTIVE at start. Waiting for activation...");
    }

    public void ActivateTreeSpawner()
    {
        if (!isActive)
        {
            isActive = true;
            Debug.Log("TreeMechanic has been ACTIVATED!");

            if (spawnCoroutine == null)
            {
                spawnCoroutine = StartCoroutine(SpawnAndDespawnTree());
            }
        }
    }

    private IEnumerator SpawnAndDespawnTree()
    {
        while (isActive)
        {
            SpawnTrees();
            yield return new WaitForSeconds(spawnInterval);
            DespawnTrees();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnTrees()
    {
        if (!isActive || ball == null || treePrefab == null) return;

        Debug.Log("Spawning Two Trees...");

        Vector3 spawnPosition1 = GetRandomSpawnPosition();
        Vector3 spawnPosition2 = GetRandomSpawnPosition();

        while (Vector3.Distance(spawnPosition1, spawnPosition2) < 2f)
        {
            spawnPosition2 = GetRandomSpawnPosition();
        }

        currentTree1 = Instantiate(treePrefab, spawnPosition1, Quaternion.identity);
        currentTree2 = Instantiate(treePrefab, spawnPosition2, Quaternion.identity);

        StartCoroutine(HandleTreeAnimations(currentTree1));
        StartCoroutine(HandleTreeAnimations(currentTree2));
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        return ball.transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;
    }

    private IEnumerator HandleTreeAnimations(GameObject tree)
    {
        if (tree == null) yield break;
        Animator treeAnimator = tree.GetComponent<Animator>();

        if (treeAnimator != null)
        {
            yield return new WaitForSeconds(0.5f);
            treeAnimator.SetBool("isGrown", true);

            yield return new WaitForSeconds(0.5f);
            treeAnimator.SetBool("isShooting", true);
            SpawnApple(tree.transform.position);
        }
    }

    private void SpawnApple(Vector3 treePosition)
    {
        if (applePrefab == null) return;
        Vector3 appleSpawnPosition = treePosition + new Vector3(0, -0.5f, 0);
        Instantiate(applePrefab, appleSpawnPosition, Quaternion.identity);
    }

    private void DespawnTrees()
    {
        if (currentTree1 != null)
        {
            Debug.Log("Despawning Tree 1...");
            Destroy(currentTree1);
            currentTree1 = null;
        }

        if (currentTree2 != null)
        {
            Debug.Log("Despawning Tree 2...");
            Destroy(currentTree2);
            currentTree2 = null;
        }
    }
}