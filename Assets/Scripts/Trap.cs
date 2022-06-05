using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    
    public void EnableColliders()
    {
        _collider.enabled = true;
    }

    public void DisableColliders()
    {
        _collider.enabled = false;
    }
}
