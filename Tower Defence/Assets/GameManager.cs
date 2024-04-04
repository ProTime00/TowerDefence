using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject GameOverUI;
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
}
