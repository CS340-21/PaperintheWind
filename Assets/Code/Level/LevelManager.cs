using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public void InitializeSingleton()
    {
        if (_instance == null)
            _instance = this;

        if (Levels.Length == 0)
            Utils.Crash("level manager is missing levels");
    }

    private void Awake()
    {
        this.InitializeSingleton();

        CurrentLevel = Levels[0];
        CurrentLevel.BeginLevel();
    }

    public Level[] Levels;

    public Level CurrentLevel;

}
