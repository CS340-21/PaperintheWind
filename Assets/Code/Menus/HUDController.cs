using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    private static HUDController _instance;

    public static HUDController Instance { get { return _instance; } }

    public void InitializeSingleton()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Awake()
    {
        this.InitializeSingleton();
    }

    public TextMeshProUGUI WaitIndicator;

    public void SetWaitIndicator(string text)
    {
        WaitIndicator.text = text;
    }

    public void ToggleWaitIndicator(bool active)
    {
        WaitIndicator.gameObject.SetActive(active);
    }

    public void DisplayWaitIndicator()
    {
        StartCoroutine(StartWaitIndicator(0.3f));
    }

    private IEnumerator StartWaitIndicator(float initialDelay)
    {
        HUDController hudController = HUDController.Instance;
        yield return new WaitForSecondsRealtime(initialDelay);
        hudController.ToggleWaitIndicator(true);
        hudController.SetWaitIndicator("Ready...");
        yield return new WaitForSecondsRealtime(Constants.RespawnWaitTime);
        hudController.SetWaitIndicator("Go!");
        yield return new WaitForSecondsRealtime(Constants.RespawnWaitTime);
        hudController.ToggleWaitIndicator(false);
    }

}
