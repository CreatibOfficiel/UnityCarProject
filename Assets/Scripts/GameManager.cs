using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    private Difficulties currentDifficulty;
    private GameMode currentGameMode;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Erreur double game manager");
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        currentDifficulty = Difficulties.EASY;
        currentGameMode = GameMode.STANDART;
    }

    public enum Difficulties
    {
        EASY,
        DIFFICULT
    }

    public enum GameMode
    {
        STANDART,
        CHRONOMODE
    }

    public Difficulties difficulties
    {
        get { return currentDifficulty; }
        set { currentDifficulty = value; }
    }

    public GameMode gamemode
    {
        get { return currentGameMode; }
        set { currentGameMode = value; }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
