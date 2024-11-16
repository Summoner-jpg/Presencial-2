using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool _isActive = false;
    
    public void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }
}
