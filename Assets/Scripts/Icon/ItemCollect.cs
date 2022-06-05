using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private RectTransform ownRectTransform;
    [SerializeField] private float lerpDuration = 0.5f;
    
    public RectTransform RectTransform { get { return ownRectTransform; } set { ownRectTransform = value; } }

    private Vector3 targetScreenPoint;
    private Vector3 startPosition;

    private clothUI clothUI;
    
    private void Start()
    {
        clothUI = transform.parent.GetComponent<clothUI>();
        targetScreenPoint = clothUI.GetRectPosition;
        startPosition = ownRectTransform.position;

        StartCoroutine(LerpPosition());
    }


    IEnumerator LerpPosition()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            ownRectTransform.position = Vector3.Lerp(startPosition, targetScreenPoint, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        clothUI.PlayCollectAnimation();
        Destroy(gameObject);
    }


}
