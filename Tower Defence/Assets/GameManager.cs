using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool ended;
    private void Start()
    {
        Debug.Log("Change that to a better solution in the future");
    }

    private void Update()
    {
        if (ended)
        {
            return;
        }
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
            ended = true;
        }
    }

    private void EndGame()
    {
        Debug.Log("the game has ended");
    }
}
