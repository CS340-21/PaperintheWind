using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{

    private static MenuController _instance;

    public static MenuController Instance { get { return _instance; } }

    public void InitializeSingleton()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Awake()
    {
        this.InitializeSingleton();
    }

    // Element [0] is normal, element [1] is ripped
    public List<Image> PlayButtonImage;
    public List<Image> QuitButtonImage;

    public Image LogoImage;

    public TextMeshProUGUI CollisionResultText;
    public TextMeshProUGUI ScoreResultText;

    public void ShowDeathScreen(int score)
    {
        gameObject.SetActive(true);
        CollisionResultText.gameObject.SetActive(true);
        ScoreResultText.gameObject.SetActive(true);
        ScoreResultText.text = string.Format("{0} points!", score);
    }

    public void PlayButtonPressed()
    {
        PlayButtonImage[0].gameObject.SetActive(false);
        PlayButtonImage[1].gameObject.SetActive(true);
        StartCoroutine(BeginGameAfterDelay(0.3f));
        HUDController.Instance.DisplayWaitIndicator();
    }

    private IEnumerator BeginGameAfterDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (PlayerManager.Instance.Controller.DeathTime > 0f)
        {
            LevelManager.Instance.CurrentLevel.RestartLevel();
        }

        gameObject.SetActive(false);
        PlayButtonImage[0].gameObject.SetActive(true);
        PlayButtonImage[1].gameObject.SetActive(false);
        PlayerManager.Instance.Controller.Revive();
    }

    public void QuitButtonPressed()
    {
        Debug.Log("quit button");
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }
}
