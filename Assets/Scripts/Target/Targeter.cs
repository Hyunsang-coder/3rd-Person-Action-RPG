using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targetList = new List<Target>{};

    
    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
        
        targetList.Add(target);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
        
        targetList.Remove(target);
    }

}
