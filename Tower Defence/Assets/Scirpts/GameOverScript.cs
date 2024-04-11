using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Text roundsText;
    public SceneFader sf;

    private void OnEnable()
    {
        roundsText.text = $"{PlayerStats.Rounds}";
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        sf.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Menu()
    {
        sf.FadeTo(0);
    }
}
