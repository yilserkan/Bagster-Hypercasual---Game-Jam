using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveCloths : MonoBehaviour
{
    
    [SerializeField] float time =0.1f;
    [SerializeField] Ease animEase;
  

    void Start()
    {
        MoveingClotsh();
    }

    private void MoveingClotsh()
    {
        transform
        .DOMoveY(-16.8f,time)
        .SetEase(animEase)
        .SetLoops(-1,LoopType.Yoyo)
        ;
        
    }
}
