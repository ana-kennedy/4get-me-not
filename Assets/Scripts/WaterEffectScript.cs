using System.Collections;
using UnityEngine;

public class WaterSplashSpawner : MonoBehaviour
{
    [Header("Splash Settings")]
    public GameObject splashPrefab;
    public float spawnInterval = 0.3f;
    public float destroyDelay = 2f;

    private Transform ball;
    private Transform balto;
    private Coroutine splashRoutine;
    private bool isBallInWater = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            ball = other.transform;
            isBallInWater = true;
            splashRoutine = StartCoroutine(SpawnSplashes());
        }
        if (other.CompareTag("Balto"))
        {
            balto = other.transform;
            isBallInWater = true;
            splashRoutine = StartCoroutine(SpawnSplashes());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            isBallInWater = false;
            if (splashRoutine != null)
            {
                StopCoroutine(splashRoutine);
            }
        }
        if(other.CompareTag("Balto"))
        {
            isBallInWater = false;
            if (splashRoutine != null)
            {
                StopCoroutine(splashRoutine);
            }
        }
    }

    private IEnumerator SpawnSplashes()
    {
        Vector3 lastBallPosition = ball != null ? ball.position : Vector3.zero;
        Vector3 lastBaltoPosition = balto != null ? balto.position : Vector3.zero;

        while (isBallInWater)
        {
            if (ball != null)
            {
                float ballDistanceMoved = Vector3.Distance(ball.position, lastBallPosition);
                if (ballDistanceMoved > 0.1f)
                {
                    GameObject splash = Instantiate(splashPrefab, ball.position, Quaternion.identity);
                    Destroy(splash, destroyDelay);
                    lastBallPosition = ball.position;
                }
            }

            if (balto != null)
            {
                float baltoDistanceMoved = Vector3.Distance(balto.position, lastBaltoPosition);
                if (baltoDistanceMoved > 0.1f)
                {
                    GameObject splash = Instantiate(splashPrefab, balto.position, Quaternion.identity);
                    Destroy(splash, destroyDelay);
                    lastBaltoPosition = balto.position;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}