using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForTimeline());
    }
    
    IEnumerator WaitForTimeline()
    {
        yield return new WaitForSeconds(3.3f);
        SceneManager.LoadScene(12);
    }
}
