using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    private int initAmount = 30;
    private float plotSize = 20f;
    private float xPosLeft = -31.95f;
    private float xPosRight = 31.95f;
    private float lastZPos = 0f;

    public List<GameObject> plots;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlot();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPlot()
    {
        int rand = Random.Range(0, plots.Count);
        GameObject plotLeft = plots[rand];
        GameObject plotRight = plots[rand];

        float zPos = lastZPos + plotSize;

        Instantiate(plotLeft, new Vector3(xPosLeft, -3f, zPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight, -3f, zPos), Quaternion.Euler(0, 90, 0));

        lastZPos += plotSize;
    }
}