using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }

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

        if (Controller == null)
            throw new Exception("player manager is missing player controller");
    }

    public PlayerController Controller;

}
