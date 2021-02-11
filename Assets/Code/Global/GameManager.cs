using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public void InitializeSingleton()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Awake()
    {
        this.InitializeSingleton();
    }

}
