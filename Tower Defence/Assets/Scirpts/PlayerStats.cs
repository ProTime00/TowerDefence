using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 1000;
    public static int Lives;
    public int startLives = 20;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
    
    
}
