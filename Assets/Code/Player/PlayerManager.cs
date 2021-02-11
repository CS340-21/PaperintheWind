using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }

    public void InitializeSingleton()
    {
        if (_instance == null)
            _instance = this;

        if (Controller == null)
            Utils.Crash("player manager is missing player controller");
    }

    private void Awake()
    {
        this.InitializeSingleton();
    }

    public PlayerController Controller;

}
