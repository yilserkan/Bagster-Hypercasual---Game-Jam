using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    private void Start()
    {
        MyColissionHandler.OnPlayerDeath += HandleOnPlayerDeath;
        int level = SceneManager.GetActiveScene().buildIndex;
        levelText.text = $"Level {level}";
    }

    private void OnDestroy()
    {
        MyColissionHandler.OnPlayerDeath -= HandleOnPlayerDeath;
    }

    private void HandleOnPlayerDeath()
    {
        Invoke(nameof(RelaodScene), 2f);
    }

    public void CallLoadNextLevel()
    {
        Invoke(nameof(LoadNextLevel), 2f);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void RelaodScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandlePlayerDeath()
    {
        Invoke(nameof(RelaodScene), 2f);
    }
}
