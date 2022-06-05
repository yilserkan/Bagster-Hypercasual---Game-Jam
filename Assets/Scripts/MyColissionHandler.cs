using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MyColissionHandler : MonoBehaviour
{
    [SerializeField] private AnimationHandler animationHandler;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private MySceneManager _sceneManager;

    [SerializeField] private ParticleSystem playerSplashParticles;
    [SerializeField] private GameObject playerVisuals;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject succesScreen;
    
    [SerializeField] private Transform splashParticlePosition;
    [SerializeField] private Transform porchParticlePosition;
    [SerializeField] private GameObject clothIconPrefab;
    [SerializeField] private GameObject clothIconParent;
    
    [SerializeField] private int level = 1;
    [SerializeField] private TMP_Text uiLevel;

    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    [SerializeField] private float increaseAmount;
    [SerializeField] private float lerpDuration;

    private int prevScaleAmount = 0;
    private int currScaleAmount = 0;

    private float startScale;

    public static event Action OnPlayerDeath;

    private bool isDead = false;

    private void Start() 
    {
        EatableBags.OnPlayerDamage  += HandleOnPlayerDamage;
        Obstacle.OnPlayerLevelChange += HandleOnPlayerDamage;
        Obstacle.OnPlayerLevelChangeWater += HandleOnPlayerDamageWater;
        
        Obstacle.OnPlayerDeath += HandleOnPlayerDeath;
        EatableBags.OnPlayerDeath += HandleOnPlayerDeath;
        Boss.OnPlayerDeath += HandleOnPlayerDeath;
        MiniBoss.OnPlayerDeath += HandleOnPlayerDeath;

        startScale = transform.localScale.x;
    }

    private void HandlePlayerDeath()
    {
        throw new NotImplementedException();
    }


    private void OnDestroy() 
    {
        EatableBags.OnPlayerDamage  -= HandleOnPlayerDamage;   
        Obstacle.OnPlayerLevelChange -= HandleOnPlayerDamage;
        Obstacle.OnPlayerLevelChangeWater -= HandleOnPlayerDamageWater;
        
        Obstacle.OnPlayerDeath -= HandleOnPlayerDeath;
        EatableBags.OnPlayerDeath -= HandleOnPlayerDeath;
        Boss.OnPlayerDeath -= HandleOnPlayerDeath;
        MiniBoss.OnPlayerDeath -= HandleOnPlayerDeath;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        EatableBags eatableBags = other.GetComponent<EatableBags>();
        
        if(eatableBags)
        {
            animationHandler.StartEatAnimation();
            //Destroy(other.gameObject);
            
             eatableBags.TryEating(level, porchParticlePosition.position);
             
        }

        if(other.GetComponent<Obstacle>())
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();
            obstacle.OnObstacleCollide(splashParticlePosition.position);
        }

        if(other.gameObject.GetComponent<EatableCloth>())
        {
            animationHandler.StartEatAnimation();
            EatableCloth eatableCloth = other.gameObject.GetComponent<EatableCloth>();
            Destroy(eatableCloth.gameObject);
            level++;
            UpdateLevelUI();
            
            Vector3 iconSpawnScreenPosition = Camera.main.WorldToScreenPoint(other.transform.position);
            GameObject spawnedIcon = Instantiate(clothIconPrefab, Vector3.zero, Quaternion.identity, clothIconParent.transform);
            spawnedIcon.GetComponent<ItemCollect>().RectTransform.position = iconSpawnScreenPosition;
            
            scoreHandler.IncreaseScore(1);

        }

        if (other.gameObject.CompareTag("Makas"))
        {
            Obstacle obstacle = other.gameObject.GetComponentInParent<Obstacle>();
            obstacle.Makas();
        }

        if (other.gameObject.GetComponent<Boss>())
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            boss.TryEating(level, porchParticlePosition.position);
        }
        if (other.gameObject.GetComponent<MiniBoss>())
        {
            MiniBoss boss = other.gameObject.GetComponent<MiniBoss>();
            boss.TryEating(level, porchParticlePosition.position);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            succesScreen.SetActive(true);
            GetComponent<PlayerMovement>().CancelPlayerControls();
            _sceneManager.CallLoadNextLevel();
        }
    }

    private void UpdateLevelUI()
    {
        uiLevel.text = $"Level {level}";
    }
    
    public void ChangeSize(int amount)
    {
        if(gameObject.transform.localScale.x > maxSize || 
           gameObject.transform.localScale.y > maxSize || 
           gameObject.transform.localScale.z > maxSize ||
           gameObject.transform.localScale.x < minSize || 
           gameObject.transform.localScale.y < minSize || 
           gameObject.transform.localScale.z < minSize ) { return; }

        float sign = Mathf.Sign(currScaleAmount - prevScaleAmount);
        Debug.Log(sign);
        prevScaleAmount = currScaleAmount;
        float startValue = gameObject.transform.localScale.x;
        float endValue = startScale + ( increaseAmount * currScaleAmount);
        
        Debug.Log(startValue);
        Debug.Log(endValue);
        StartCoroutine(LerpSize(startValue,endValue));
        
    }
    
    
    private IEnumerator LerpSize(float startValue,float endValue)
    {
        float timeElapsed = 0;

        Vector3 temp = Vector3.zero;
        while (timeElapsed < lerpDuration)
        {
            temp.x = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration );
            temp.y = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration );
            temp.z = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration );

            transform.localScale = temp;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(endValue, endValue, endValue);
    }

    public void HandleOnPlayerDamage(int amount)
    {
        ChangeLevel(amount);
        UpdateLevelUI();
    }
    private void HandleOnPlayerDamageWater(int amount)
    {
        if (level + amount <= 0)
        {
            level = 1;
            UpdateLevelUI();
            return;
        }
        ChangeLevel(amount);
        UpdateLevelUI();
    }

    private void ChangeLevel(int amount)
    {
        if (level + amount <= 0 )
        {
            level = 0;
            UpdateLevelUI();
            HandleOnPlayerDeath();
            return;
        }
        level += amount;
        
        currScaleAmount = level / 10;
        if(currScaleAmount == prevScaleAmount) { return; }
        ChangeSize(amount);
    }

    public void IncreaseLevel()
    {
        level++;
        UpdateLevelUI();
    }

    private void HandleOnPlayerDeath()
    {
        if(isDead) { return; }

        isDead = true;
        Instantiate(playerSplashParticles, transform.position, Quaternion.identity);
        playerVisuals.SetActive(false);
        failScreen.SetActive(true);
        OnPlayerDeath?.Invoke();
    }
    
        
}
