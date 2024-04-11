using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public SceneFader sf;
    public GameObject GameOverUI;
    public GameObject WinUI;
    public static bool ended;
    
    private void Start()
    {
        
        ended = false;
        GameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (ended)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }
        
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        ended = true;
        GameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("end level");
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex);
        WinUI.SetActive(true);
    }
}
