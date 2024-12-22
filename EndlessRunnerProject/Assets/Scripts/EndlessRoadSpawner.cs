using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessRoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    public List<GameObject> roads2;
    public List<GameObject> roads3;

    private float offset = 40f;

    private float elapsedTime = 0f; // Tracks the elapsed game time
    private int currentPhase = 1; // Tracks the current difficulty phase

    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Update the phase based on elapsed time
        if (elapsedTime > 60f && currentPhase == 1) // After 60 seconds, switch to Phase 2
        {
            currentPhase = 2;
        }
        else if (elapsedTime > 120f && currentPhase == 2) // After 120 seconds, switch to Phase 3
        {
            currentPhase = 3;
        }
    }

    public void MoveRoad()
    {
        GameObject moveRoad = roads[0];
        roads.Remove(moveRoad);

        float newZ = roads[roads.Count - 1].transform.position.z + offset;

        // Select road lists based on the current phase
        switch (currentPhase)
        {
            case 1:
                moveRoad = SelectRoadFromList(roads);
                break;

            case 2:
                moveRoad = SelectRoadFromList(roads2);
                break;

            case 3:
                moveRoad = SelectRoadFromList(roads3);
                break;

            default:
                moveRoad = SelectRoadFromList(roads); // Default to Phase 1 roads
                break;
        }

        moveRoad.transform.position = new Vector3(1, -3, newZ);
        roads.Add(moveRoad);
    }

    private GameObject SelectRoadFromList(List<GameObject> roadList)
    {
        // Select a random road from the list
        if (roadList != null && roadList.Count > 0)
        {
            return roadList[Random.Range(0, roadList.Count)];
        }
        return null;
    }
}
