using System.Collections.Generic;
using UnityEngine;

public class CreateRealTimeObstacles : MonoBehaviour
{
    // Prefabs
    [Header("Prefabs")]
    [SerializeField]
    private GameObject road;
    [SerializeField]
    private GameObject limitOfRoad;
    [SerializeField]
    private GameObject parentObstacles;
    [SerializeField]
    private List<GameObject> listOfObstacles;
    [SerializeField]
    private List<float> listOfProbabilities;

    private float minimumInterval;

#if UNITY_EDITOR
    void OnValidate()
    {
        // Check if each obstacle will be assigned to aprobability
        if (!(listOfObstacles.Count == listOfProbabilities.Count))
            Debug.LogError("CreateRealTimeObstacles : " + listOfObstacles.Count + " != " + listOfProbabilities.Count);
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateObstacle());
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


    System.Collections.IEnumerator generateObstacle()
    {
        while (true)
        {
            // Time to wait before a new spawn of an obstacle (max 3s - min 0.25s)
            if(GameManager.GetInstance().difficulties == GameManager.Difficulties.EASY)
            {
                minimumInterval = 1.0f;
            }

            if (GameManager.GetInstance().difficulties == GameManager.Difficulties.DIFFICULT)
            {
                minimumInterval = 0.25f;
            }

            yield return new WaitForSeconds(Mathf.Max(3.0f - (transform.position.z * 0.01f), minimumInterval));

            GameObject obstacle = listOfObstacles[GetRandomWeightedIndex(listOfProbabilities)];

            float roadWidth = road.GetComponent<Renderer>().bounds.size.x / 2;
            float randomX = Random.Range(-roadWidth + 2.0f, +roadWidth - 2.0f);
            float randomZ = Random.Range(transform.position.z + 10, limitOfRoad.transform.position.z - 2);

            if(obstacle.name == "StarPrefab")
                GameObject.Instantiate(obstacle, new Vector3(randomX, 3, randomZ), Quaternion.Euler(new Vector3(0, 180, 0)), parentObstacles.transform);
            else GameObject.Instantiate(obstacle, new Vector3(randomX, 0, randomZ), Quaternion.Euler(new Vector3(0, 180, 0)), parentObstacles.transform);
        }
    }
}
