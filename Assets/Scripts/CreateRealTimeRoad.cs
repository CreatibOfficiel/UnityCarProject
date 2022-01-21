using System.Collections.Generic;
using UnityEngine;

public class CreateRealTimeRoad : MonoBehaviour
{

    // Prefabs
    [Header("Prefabs")]
    [SerializeField]
    private List<GameObject> listOfRoadPattern = new List<GameObject>();
    [SerializeField]
    private GameObject limitOfRoad;
    [SerializeField]
    private GameObject parentRoad;

    private List<GameObject> listOfRoad = new List<GameObject>();
    private float distance;
    public int numberOfRoadAtStart;

    [SerializeField]
    private List<float> listOfProbabilities;

#if UNITY_EDITOR
    void OnValidate()
    {
        // Check if each obstacle will be assigned to aprobability
        if (!(listOfRoadPattern.Count == listOfProbabilities.Count))
            Debug.LogError("CreateRealTimeObstacles : " + listOfRoadPattern.Count + " != " + listOfProbabilities.Count);
    }
#endif

    private void Awake()
    {
        for (int i = 0; i < numberOfRoadAtStart; ++i)
        {
            GameObject go = Instantiate(listOfRoadPattern[(int)Mathf.Max(2.0f - (distance * 0.01f), 0)], new Vector3(0, 0, i * listOfRoadPattern[0].GetComponent<Renderer>().bounds.size.z), Quaternion.Euler(new Vector3(0, 90, 0)), parentRoad.transform);
            listOfRoad.Add(go);
        }
        limitOfRoad.transform.position = new Vector3(0, 0, listOfRoad.Count * listOfRoadPattern[0].GetComponent<Renderer>().bounds.size.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (limitOfRoad.transform.position.z - transform.position.z < 50)
        {
            GenerateNewRoad();
        }
        
        GameObject lastRoad = listOfRoad[0];

        if (transform.position.z - lastRoad.transform.position.z > 20)
        {
            DeleteOldRoad(lastRoad);
        }
    }

    void GenerateNewRoad()
    {
        //int choice = (int)Mathf.Max(2.0f - (distance * 0.01f), 0);
        //GameObject go = GameObject.Instantiate(listOfRoadPattern[choice], new Vector3(0, 0, limitOfRoad.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)), parentRoad.transform);
        GameObject road = GameObject.Instantiate(listOfRoadPattern[GetRandomWeightedIndex(listOfProbabilities)], new Vector3(0, 0, limitOfRoad.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)), parentRoad.transform);
        limitOfRoad.transform.position = new Vector3(0, 0, limitOfRoad.transform.position.z + 5);
        listOfRoad.Add(road);
        distance += 5;
    }

    void DeleteOldRoad(GameObject lastRoad)
    {
        listOfRoad.RemoveAt(0);
        Destroy(lastRoad);
    }

    // Random number with weight
    // From : https://forum.unity.com/threads/random-numbers-with-a-weighted-chance.442190/#post-2859582
    public int
    GetRandomWeightedIndex(List<float> weights)
    {
        if (weights == null || weights.Count == 0)
            return -1;

        float totalWeight = 0.0f;
        for (int i = 0; i < weights.Count; i++)
        {
            float iWeight = weights[i];

            if (float.IsPositiveInfinity(iWeight))
                return i;

            else if (iWeight >= 0f && !float.IsNaN(iWeight))
                totalWeight += weights[i];
        }

        float random = Random.value;
        float indexToTest = 0.0f;

        for (int i = 0; i < weights.Count; i++)
        {
            float iWeight = weights[i];
            if (float.IsNaN(iWeight) || iWeight <= 0f)
                continue;

            indexToTest += iWeight / totalWeight;
            if (indexToTest >= random)
                return i;
        }
        return -1;
    }
}

