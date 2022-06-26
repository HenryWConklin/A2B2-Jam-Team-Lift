using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 
    public GameObject mainMenuObj;
    public GameObject gameOverObj;


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
    }

    public void StartButton_Pressed()
    {
        mainMenuObj.SetActive(false);
        GameManager.Instance.StartGame();
        
    }


    public void DisplayGameOverScreen()
    {
        gameOverObj.SetActive(true);
    }
}
