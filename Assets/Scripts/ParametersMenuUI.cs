using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametersMenuUI : MonoBehaviour
{

    [SerializeField]
    Toggle chronoModeButton;

    public void setDifficultyDifficult()
    {
        GameManager.GetInstance().difficulties = GameManager.Difficulties.DIFFICULT;
        Debug.Log(GameManager.GetInstance().gamemode);
    }

    public void setDifficultyEasy()
    {
        GameManager.GetInstance().difficulties = GameManager.Difficulties.EASY;
    }

    public void setChronoMode()
    {
        GameManager.GetInstance().gamemode = chronoModeButton.isOn ? GameManager.GameMode.CHRONOMODE : GameManager.GameMode.STANDART;
    }
}
