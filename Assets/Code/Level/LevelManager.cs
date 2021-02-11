using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if (Levels.Length == 0)
            Utils.Crash("level manager is missing levels");

        CurrentLevel = Levels[0];
        CurrentLevel.BeginLevel();
    }

    public Level[] Levels;

    public Level CurrentLevel;

}
