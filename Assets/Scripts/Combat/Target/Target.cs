using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    public void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

}
