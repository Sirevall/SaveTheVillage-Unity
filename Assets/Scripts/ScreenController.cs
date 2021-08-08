using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject GameScreen;
    public GameObject ResultScreen;

    private void Start()
    {
        StartScreen.SetActive(true);
    }
    public void EnableGameScreen()
    {
        StartScreen.SetActive(false);
        ResultScreen.SetActive(false);
        GameScreen.SetActive(true);
    }
    public void EnableEndScreen()
    {
        GameScreen.SetActive(false);
        ResultScreen.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
