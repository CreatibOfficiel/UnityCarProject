using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsManager : MonoBehaviour
{

    private static StarsManager instance;
    private int maxPoint = 12, currentPoint = 12, currentMaxSpeed;
    private List<int> listOfMaxSpeed = new List<int> { 10, 25, 30 };
    [SerializeField]
    private Text starsText, speedText;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("plusieurs instance StarsManager");
            return;
        }
        instance = this;
    }

    void Start()
    {
        chooseAllowedSpeed();
    }

    private void Update()
    {
        speedText.text = PlayerController_Physique13.getInstance().getCurrentSpeed().ToString() + " Km/h";
    }

    void updateText()
    {
        starsText.text = "";

        for(int i = 0; i < maxPoint - currentPoint; ++i)
            starsText.text += "#";
        for (int j = 0; j < currentPoint; ++j)
            starsText.text += "*";

        starsText.text += "\nVitesse max autorisée : " + currentMaxSpeed.ToString();
    }

    public void removePoint(float speedCurrent)
    {
        float diff = currentMaxSpeed - speedCurrent;
        int numberOfRemovedPoint = 0;

        if (diff > 5 && diff < 20)
        {
            numberOfRemovedPoint = 1;
        } else if(diff < 30)
        {
            numberOfRemovedPoint = 2;
        } else if (diff < 40)
        {
            numberOfRemovedPoint = 3;
        } else if(diff < 50)
        {
            numberOfRemovedPoint = 4;
        } else if(diff >= 50)
        {
            numberOfRemovedPoint = 6;
        }

        if (currentPoint - numberOfRemovedPoint <= 0)
            currentPoint = 0;
        else currentPoint -= numberOfRemovedPoint;

        if (currentPoint == 0)
            PlayerController_Physique13.getInstance().death();

        updateText();
    }

    public void addPoint()
    {
        if(currentPoint + 1 < maxPoint)
            ++currentPoint;

        updateText();
    }

    public int getCurrentPoint()
    {
        return currentPoint;
    }

    public void chooseAllowedSpeed()
    {
        int random = Random.Range(0, listOfMaxSpeed.Count);
        currentMaxSpeed = listOfMaxSpeed[random];
        updateText();
    }

    public static StarsManager GetInstance()
    {
        return instance;
    }

    public int getAllowedSpeed()
    {
        return currentMaxSpeed;
    }
}
