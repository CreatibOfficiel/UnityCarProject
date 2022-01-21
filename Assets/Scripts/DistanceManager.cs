using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Text distanceText;
    [SerializeField]
    DataScore maxDistanceData;

    // Update is called once per frame
    void Update()
    {
        distanceText.text = "Distance : " + player.transform.position.z.ToString() + "\n" + "Record : " + maxDistanceData.maxDistanceStandart.ToString();
    }
}
