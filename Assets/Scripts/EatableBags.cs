using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class EatableBags : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int score;
    [SerializeField] private TMP_Text uiLevel;
    [SerializeField] private ParticleSystem slashParticles;

    public static event Action OnPlayerDeath;
    public static event Action<int> OnPlayerDamage;
    public static event Action OnPlayerSlowMovement;

    public int GetScore { get{ return score; } }

    private void Start() 
    {
         uiLevel.text = $"Level {level}";  
    }

    public bool TryEating(int playerLevel, Vector3 slashPosition)
    {
        if(playerLevel < level)
        {
            
            int difference = playerLevel - level;
            if(playerLevel - difference <= 0)
            {
                OnPlayerDeath?.Invoke();
                return false;
            }
            
            
            OnPlayerDamage?.Invoke(difference);
        }
        else 
        {
           OnPlayerDamage?.Invoke(level);
           OnPlayerSlowMovement?.Invoke();
           Destroy(gameObject);
           Instantiate(slashParticles, slashPosition, Quaternion.identity);
           return true;
        }
        return true;
    }
}
