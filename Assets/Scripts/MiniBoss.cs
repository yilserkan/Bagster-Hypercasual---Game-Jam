using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int score;
    [SerializeField] private TMP_Text uiLevel;
    [SerializeField] private ParticleSystem slashParticles;
    [SerializeField] private MySceneManager _sceneManager;
    [SerializeField] private GameObject succesScreen;
    [SerializeField] private PlayerMovement _playerMovement;

    public static event Action OnPlayerDeath;
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
            OnPlayerDeath?.Invoke();
        }
        else 
        {
            OnPlayerSlowMovement?.Invoke();
            Destroy(gameObject);
            Instantiate(slashParticles, slashPosition, Quaternion.identity);
            succesScreen.SetActive(true);
            _playerMovement.CancelPlayerControls();
            _sceneManager.CallLoadNextLevel();
            //player MoveTO
            return true;
        }
        return true;
    }
}
