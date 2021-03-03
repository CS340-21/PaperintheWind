using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDDebug : MonoBehaviour
{

    private List<PlayerHUDDebugText> Messages = new List<PlayerHUDDebugText>();

    private List<PlayerHUDDebugText> TempMessages = new List<PlayerHUDDebugText>();

    public GUIStyle style = new GUIStyle();

    private int width, height;

    private float deltaTime = 0.0f;

    public void PushText(string text)
    {
        TempMessages.Add(new PlayerHUDDebugText(UnityEngine.Random.Range(1, 10000).ToString(), text, Time.unscaledTime + 5));
    }

    public void UpdateText(string id, string newText)
    {
        int index = Messages.FindIndex(item => item.ID == id);
        if (index == -1)
        {
            Messages.Add(new PlayerHUDDebugText(id, newText, 0f));
        }
        else
        {
            Messages[index].Text = newText;
        }
    }

    void Awake()
    {
        width = Screen.width;
        height = Screen.height;

        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = height * 2 / 100;
        style.normal.textColor = new Color(1f, 1f, 1f, 1.0f);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Draw fps counter
        Rect rect = new Rect(50, 150, width, height * 2 / 100);
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", deltaTime * 1000.0f, 1.0f / deltaTime);
        GUI.Label(rect, text, style);

        int y = 150;

        foreach (PlayerHUDDebugText msg in Messages)
        {
            rect = new Rect(50, y += 50, width, height * 2 / 100);
            GUI.Label(rect, msg.Text, style);
        }

        for (int i = TempMessages.Count - 1; i >= 0; i--)
        {
            PlayerHUDDebugText msg = TempMessages[i];

            if (msg.ExpirationTime != 0 && msg.IsExpired())
            {
                TempMessages.RemoveAt(i);
                continue;
            }

            rect = new Rect(50, y += 50, width, height * 2 / 100);
            GUI.Label(rect, msg.Text, style);
        }
    }
}

public class PlayerHUDDebugText
{

    public string ID;
    public string Text;
    public float ExpirationTime;

    public PlayerHUDDebugText(string ID, string text, float expirationTime)
    {
        this.ID = ID;
        this.Text = text;
        this.ExpirationTime = expirationTime;
    }

    public bool IsExpired()
    {
        return Time.unscaledTime > this.ExpirationTime;
    }

}