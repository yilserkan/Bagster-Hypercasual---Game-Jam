using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Animation diamondAnimation;

    public Vector3 GetRectPosition { get { return rectTransform.position; } }

    public void PlayCollectAnimation()
    {
        diamondAnimation.Play();
    }
}
