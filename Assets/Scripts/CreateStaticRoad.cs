using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStaticRoad : MonoBehaviour
{

    // Prefabs
    [Header("Prefabs")]
    [SerializeField]
    private GameObject box, roadPattern, placeOfSpawnBox, parentRoad, parentBox;
    [SerializeField]
    private int numberOfRoadAtStart, numberOfBox;

    private List<Vector2> listOfSumOfPosObstacles;
    private float paternSize, paternWidth;

    private void Awake()
    {
        for (int i = 0; i < numberOfRoadAtStart; ++i)
        {
            GameObject road = GameObject.Instantiate(roadPattern, new Vector3(0, 0, i * roadPattern.GetComponent<Renderer>().bounds.size.z), Quaternion.Euler(new Vector3(0, 90, 0)), parentRoad.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        listOfSumOfPosObstacles = new List<Vector2>();

        // Get the road size
        paternSize = roadPattern.GetComponent<Renderer>().bounds.size.x;
        paternWidth = roadPattern.GetComponent<Renderer>().bounds.size.z;

        GameObject firstBox = GameObject.Instantiate(box, new Vector3(placeOfSpawnBox.transform.position.x, 0, placeOfSpawnBox.transform.position.z), Quaternion.Euler(0, 90, 0), parentBox.transform);
        listOfSumOfPosObstacles.Add(new Vector2(placeOfSpawnBox.transform.position.x, placeOfSpawnBox.transform.position.z));

        float posX, posZ;
        Vector2 vector;
        for (int j = 0; j < numberOfBox; ++j)
        {
            do
            {
                posX = Random.Range(-paternWidth / 2.0f + 2.0f, paternWidth / 2.0f - 2.0f);
                posZ = Random.Range(15f, numberOfRoadAtStart * paternSize);
                vector = new Vector2(posX, posZ);
            } while (!IsPosAreSame(vector));
            
            GameObject newBox = GameObject.Instantiate(box, new Vector3(posX, 0, posZ), Quaternion.Euler(0, 90, 0), parentBox.transform);
            //newBox.transform.SetParent(transform);
        }
    }

    bool IsPosAreSame(Vector2 pos)
    {
        bool result = true;
        
        if (listOfSumOfPosObstacles.Count != 0)
        {
            for (int i = 0; i < listOfSumOfPosObstacles.Count; ++i)
            {
                if (Vector2.Distance(pos, listOfSumOfPosObstacles[i]) < 2)
                {
                    result = false;
                    break;
                }
            }
        }
        else listOfSumOfPosObstacles.Add(pos);

        return result;
    }
}
