using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private int initAmount = 30;
    private float plotSize = 10.3f;
    private float xPos = 0f;
    private float lastZPos = 4.3f;

    public List<GameObject> obstacles;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnObstacle();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObstacle()
    {
        int rand = Random.Range(0, obstacles.Count);
        GameObject plotObstacles = obstacles[rand];

        float zPos = lastZPos + plotSize;

        GameObject spawnedObstacle = Instantiate(plotObstacles, new Vector3(xPos, -3.1f, zPos), plotObstacles.transform.rotation);
       

        lastZPos += plotSize;
    }
}