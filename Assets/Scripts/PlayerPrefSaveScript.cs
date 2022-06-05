using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefSaveScript : MonoBehaviour
{
    private const string MoneKey = "MoneyKey";

    public float Money
    {
        get => PlayerPrefs.GetFloat(MoneKey, 0f);
        private set => PlayerPrefs.SetFloat(MoneKey, value);
    }
    
    public void SetMoney(float amount)
    {
        Money = amount;
    }
    
}
