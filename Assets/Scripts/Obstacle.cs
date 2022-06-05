using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleTypes obstacleType;
    [SerializeField] private int levelDrop;
    [SerializeField] private ParticleSystem splashParticleEffect;
    [SerializeField] private Transform splashPosition;
    
    public static event Action OnPlayerDeath;
    public static event Action<int> OnPlayerLevelChange;
    public static event Action<int> OnPlayerLevelChangeWater;
    public static event Action OnPlayerSlowMovement;
    
    public void OnObstacleCollide(Vector3 hitPosition)
    {
        switch (obstacleType)
        {
            case ObstacleTypes.Ogutucu:
                Ogutucu();
                break;
            case ObstacleTypes.Makas:
                Makas();
                break;
            case ObstacleTypes.Spike:
                Spike();
                break;
            case ObstacleTypes.Water:
                Water(hitPosition);
                break;
            case ObstacleTypes.Lav:
                Lav(hitPosition);
                break;
        }
    }

    private void Water(Vector3 hitPosition)
    {
        OnPlayerLevelChangeWater?.Invoke(-levelDrop);
        OnPlayerSlowMovement?.Invoke();
        hitPosition.z += 2f;
        ParticleSystem splahParticles = Instantiate(splashParticleEffect, hitPosition, Quaternion.identity);
        
        Destroy(splahParticles , 2f);
    }
    private void Lav(Vector3 hitPosition)
    {
        OnPlayerLevelChange?.Invoke(-levelDrop);
        OnPlayerSlowMovement?.Invoke();
        hitPosition.z += 2f;
        ParticleSystem splahParticles = Instantiate(splashParticleEffect, hitPosition, Quaternion.identity);
        
        Destroy(splahParticles , 2f);
    }
    private void Ogutucu()
    {
        Debug.Log("ogutucu");
   
        //Play Die Aniim
   
        OnPlayerDeath?.Invoke();
    }

    public void Makas()
    {
        Debug.Log("Makas");
        OnPlayerLevelChange?.Invoke(-levelDrop);
    }

    private void Spike()
    {
        OnPlayerLevelChange?.Invoke(-levelDrop);
    }
}
