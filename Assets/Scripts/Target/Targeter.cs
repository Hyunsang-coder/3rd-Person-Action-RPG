using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] List<Target> targetList = new List<Target>{};
    public Target target;

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

    public bool LockOnTarget()
    {
        if (targetList.Count == 0) return false;

        target = targetList[0]; 
        return true;
    }

}
