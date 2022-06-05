using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemativeBagActivate : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();   
    }

    public void EnableBag()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
    public void DisableBag()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
