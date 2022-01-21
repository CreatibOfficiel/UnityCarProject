using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{

    [SerializeField]
    Text textScore;
    [SerializeField]
    DataScore maxDistanceData;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GetInstance().gamemode == GameManager.GameMode.STANDART)
            textScore.text = "Distance max : " + maxDistanceData.maxDistanceStandart.ToString();


        if (GameManager.GetInstance().gamemode == GameManager.GameMode.CHRONOMODE)
            textScore.text = "Distance max : " + maxDistanceData.maxDistanceChrono.ToString();

        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(5.0f);
        SceneController.GetInstance().loadScene(0);
    }
}
