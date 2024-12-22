using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessObstacleSpawner : MonoBehaviour
{
    private float plotSize = 10.3f;
    private float xPos = 0f;
    private float lastZPos = 4.3f;

    public List<GameObject> obstacles;
    public List<GameObject> obstacles2;
    public List<GameObject> obstacles3;

    private float elapsedTime = 0f; // Tracks elapsed game time
    private int currentPhase = 1; // Tracks the current difficulty phase

    public int obstaclesPerInterval = 10; // Number of obstacles to spawn in each interval
    public float spawnInterval = 10f; // Time between intervals in seconds

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning obstacles at intervals
        StartCoroutine(SpawnObstaclesAtIntervals());
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Update phase based on elapsed time
        if (elapsedTime > 60f && currentPhase == 1) // After 60 seconds, switch to Phase 2
        {
            currentPhase = 2;
        }
        else if (elapsedTime > 120f && currentPhase == 2) // After 120 seconds, switch to Phase 3
        {
            currentPhase = 3;
        }
    }

    private IEnumerator SpawnObstaclesAtIntervals()
    {
        while (true)
        {
            for (int i = 0; i < obstaclesPerInterval; i++)
            {
                SpawnObstacle();
            }
            yield return new WaitForSeconds(spawnInterval); // Wait for the interval duration
        }
    }

    public void SpawnObstacle()
    {
        GameObject selectedObstacle;

        // Select obstacle based on the current phase
        switch (currentPhase)
        {
            case 1:
                selectedObstacle = SelectObstacleFromList(obstacles);
                break;

            case 2:
                selectedObstacle = SelectObstacleFromList(obstacles2);
                break;

            case 3:
                selectedObstacle = SelectObstacleFromList(obstacles3);
                break;

            default:
                selectedObstacle = SelectObstacleFromList(obstacles);
                break;
        }

        if (selectedObstacle != null)
        {
            float zPos = lastZPos + plotSize;

            // Instantiate the selected obstacle at the specified position
            Instantiate(selectedObstacle, new Vector3(xPos, -3.1f, zPos), selectedObstacle.transform.rotation);

            lastZPos += plotSize;
        }
    }

    private GameObject SelectObstacleFromList(List<GameObject> obstacleList)
    {
        // Randomly select an obstacle from the provided list
        if (obstacleList != null && obstacleList.Count > 0)
        {
            return obstacleList[Random.Range(0, obstacleList.Count)];
        }
        return null;
    }
}
