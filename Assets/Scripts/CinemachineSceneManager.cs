using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CinemachineSceneManager : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitForTimeline());
    }
    
    IEnumerator WaitForTimeline()
    {
        yield return new WaitForSeconds(12.2f);
        SceneManager.LoadScene(1);
    }
}
