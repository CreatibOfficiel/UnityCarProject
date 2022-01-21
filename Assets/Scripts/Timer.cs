using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public Text currentTimeText;

    [SerializeField]
    private DataScore maxDistanceData;
    [SerializeField]
    private GameObject player;

    private static Timer instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("deux Timer....");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;
            if(currentTime < 0)
            {
                StopTimer();

                if (maxDistanceData.maxDistanceChrono < player.transform.position.z)
                    maxDistanceData.maxDistanceChrono = (int)player.transform.position.z;
                SceneController.GetInstance().loadScene(3);
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Temps restant : " + time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public static Timer getInstance()
    {
        return instance;
    }

    public void addTime()
    {
        if (currentTime + 5 < startMinutes * 60)
            currentTime += 5;
        else currentTime = startMinutes * 60;
    }
}
