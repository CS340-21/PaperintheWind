using System.Collections;
using System.Collections.Generic;
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

        CurrentLevel = Levels[0];
        CurrentLevel.BeginLevel();
    }

    public Level[] Levels;

    public Level CurrentLevel;

}
