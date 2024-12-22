using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlotSpawner : MonoBehaviour
{
    private int initAmount = 50;
    private float plotSize = 20f;
    private float xPosLeft = -31.95f;
    private float xPosRight = 31.95f;
    private float lastZPos = 0f;

    public List<GameObject> plots;
    public List<GameObject> plots2;
    public List<GameObject> plots3;

    private float elapsedTime = 0f; // Tracks elapsed game time
    private int currentPhase = 1; // Tracks the current difficulty phase

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

    public void SpawnPlot()
    {
        GameObject plotLeft;
        GameObject plotRight;

        // Select plots based on the current phase
        switch (currentPhase)
        {
            case 1:
                plotLeft = SelectPlotFromList(plots);
                plotRight = SelectPlotFromList(plots);
                break;

            case 2:
                plotLeft = SelectPlotFromList(plots2);
                plotRight = SelectPlotFromList(plots2);
                break;

            case 3:
                plotLeft = SelectPlotFromList(plots3);
                plotRight = SelectPlotFromList(plots3);
                break;

            default:
                plotLeft = SelectPlotFromList(plots);
                plotRight = SelectPlotFromList(plots);
                break;
        }

        float zPos = lastZPos + plotSize;

        // Spawn the plots on the left and right positions
        Instantiate(plotLeft, new Vector3(xPosLeft, -3f, zPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight, -3f, zPos), Quaternion.Euler(0, 90, 0));

        lastZPos += plotSize;
    }

    private GameObject SelectPlotFromList(List<GameObject> plotList)
    {
        // Select a random plot from the list
        if (plotList != null && plotList.Count > 0)
        {
            return plotList[Random.Range(0, plotList.Count)];
        }
        return null;
    }
}
