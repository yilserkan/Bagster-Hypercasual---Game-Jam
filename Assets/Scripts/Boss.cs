using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int score;
    [SerializeField] private TMP_Text uiLevel;
    [SerializeField] private ParticleSystem slashParticles;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MySceneManager _mySceneManager;

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
            _animator.SetTrigger("BossEat");
            OnPlayerDeath?.Invoke();
        }
        else 
        {
            OnPlayerSlowMovement?.Invoke();
            Destroy(gameObject);
            Instantiate(slashParticles, slashPosition, Quaternion.identity);
            _playerMovement.CancelPlayerControls();
            winScreen.SetActive(true);
            _mySceneManager.CallLoadNextLevel();
            //player MoveTO
            return true;
        }
        return true;
    }
}
