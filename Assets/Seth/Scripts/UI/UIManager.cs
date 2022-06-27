using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 
    public GameObject mainMenuObj;
    public GameObject gameOverObj;
    public GameObject howToPlayWindow;
    public TMP_Text scoreText;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying GameManager");
            Destroy(this.gameObject);
        }
        else
            Instance = this;
        mainMenuObj = transform.Find("MainMenu").gameObject;
        gameOverObj = transform.Find("GameOver").gameObject;
        howToPlayWindow = mainMenuObj.transform.Find("HowToPlayWindow").gameObject;
        scoreText = transform.Find("Score").Find("Text").GetComponent<TMP_Text>(); 
    }

    public void StartButton_Pressed()
    {
        mainMenuObj.SetActive(false);
        GameManager.Instance.StartGame();
        
    }

    public void ExitGame() => Application.Quit();

    public void OpenHowToPlay_Pressed() => howToPlayWindow.SetActive(true);
    public void CloseHowToPlay_Pressed() => howToPlayWindow.SetActive(false);


    public void DisplayGameOverScreen()
    {
        gameOverObj.SetActive(true);
    }

    public void UpdateScoreUI()
    {
        scoreText.text = $"Score: " + GameManager.Instance.currentScore;
    }
}
