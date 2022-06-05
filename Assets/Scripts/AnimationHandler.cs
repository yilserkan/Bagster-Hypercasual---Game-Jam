using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    

    public void StartEatAnimation()
    {
        //Start Eat Anim
        Debug.Log("Start Eat Anim");
        animator.SetTrigger("eat");
    }
    
}
