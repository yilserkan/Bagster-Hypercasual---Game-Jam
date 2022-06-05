using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    [SerializeField] private ScoreHandler _scoreHandler;
    [SerializeField] private MyColissionHandler _colissionHandler;
    
    
    private int leveBuffCount = 1;
    private int cashMultiplierCount = 1;
    
    private int multiplierBuffCount = 1;
    private float cashMultiplier = 1;

    public float GetCashMultiplier
    {
        get { return cashMultiplier; }
    }

    public void BuffLevel()
    {
        float score = _scoreHandler.GetScore;
        float price = leveBuffCount * 3;
        if (score >= price)
        {
            _scoreHandler.DecreaseScore(price);
            _colissionHandler.IncreaseLevel();
            leveBuffCount++;
        }
    }

    public void BuffCashMoneyMultiplier()
    {
        if (cashMultiplier >= 3)
        {
            return;
        }
        float score = _scoreHandler.GetScore;
        float price = cashMultiplierCount * 5;
        if (score >= price)
        {
            _scoreHandler.DecreaseScore(price);
            cashMultiplier = 1 + (cashMultiplierCount * 0.25f);
            cashMultiplierCount++;
        }
    }
    
    }

