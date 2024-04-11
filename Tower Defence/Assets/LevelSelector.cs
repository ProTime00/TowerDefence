using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader fader;
    public Button[] buttons;
    
    private void Start()
    {
        var leverReached = PlayerPrefs.GetInt("level", 1);
        for (var index = 0; index < buttons.Length; index++)
        {
            if (index + 1 > leverReached)
            {
                var button = buttons[index];
                button.interactable = false;
            }
        }
    }

    public void Select(int level)
    {
        fader.FadeTo(level);
    }
    
}
