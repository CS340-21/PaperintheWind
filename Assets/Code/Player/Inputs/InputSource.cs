using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputSource
{

    public void Process()
    {
        PlayerController controller = PlayerManager.Instance.Controller;

        // probably replace this with menu system
        if (Input.anyKey)
        {
            if (controller.DeathTime != 0)
            {
                if (Time.unscaledTime - controller.DeathTime > 1)
                    LevelManager.Instance.CurrentLevel.RestartLevel();

                return;
            }
        }

        this.ProcessInputType();
    }

    public abstract void ProcessInputType();

}
