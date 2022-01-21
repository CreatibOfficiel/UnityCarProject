using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hhelo");
        if (GameManager.GetInstance().gamemode == GameManager.GameMode.STANDART)
            StarsManager.GetInstance().addPoint();

        if (GameManager.GetInstance().gamemode == GameManager.GameMode.CHRONOMODE)
            Timer.getInstance().addTime();
        
        Destroy(transform.gameObject);
    }
}
