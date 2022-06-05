using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private float totalScore = 0;
    [SerializeField] private PlayerPrefSaveScript _saveScript;
    [SerializeField] private TMP_Text clothCount;
    [SerializeField] private Buffs _buffs;
    
    public float GetScore
    {
        get { return totalScore; }
    }

    private void Start()
    {
        totalScore = _saveScript.Money;
        UpdateClotCount();
        
    }

    public void IncreaseScore(float amount)
    {
        Debug.Log("Increase Score");
        float increaseAmount = amount * _buffs.GetCashMultiplier;
        Debug.Log(increaseAmount);
        totalScore += increaseAmount;
        _saveScript.SetMoney(totalScore);
        UpdateClotCount();
    }

    public void DecreaseScore(float amount)
    {
        Debug.Log("Decrease Score");
        if (totalScore - amount <= 0)
        {
            amount = 0;
        }
        totalScore -= amount;
        _saveScript.SetMoney(totalScore);
        UpdateClotCount();
    }

    private void UpdateClotCount()
    {
        int roundedScore = (int)totalScore;
        clothCount.text = roundedScore.ToString();
    }
}
