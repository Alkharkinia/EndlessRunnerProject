using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private int initAmount = 30;
    private float plotSize = 9.6f;
    private float xPosLeft = -3.9f;
    private float xPosRight = 3.9f;
    private float lastZPos = 15.6f;

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
        int rand2 = Random.Range(0, obstacles.Count);
        GameObject plotLeft = obstacles[rand];
        GameObject plotRight = obstacles[rand2];

        float zPos = lastZPos + plotSize;

        Instantiate(plotLeft, new Vector3(xPosLeft, -3.1f, zPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight, -3.1f, zPos), plotRight.transform.rotation);

        lastZPos += plotSize;
    }
}