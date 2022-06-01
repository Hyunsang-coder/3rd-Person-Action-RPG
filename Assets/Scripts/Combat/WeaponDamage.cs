using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{   
    [SerializeField] Collider myCollider;
    List<Collider> alreadyHit = new List<Collider>();

    PlayerStateMachine statemachine;
    int damage;

    void Awake()
    {
        statemachine = GetComponent<PlayerStateMachine>();
    }
    void OnEnable()
    {
        alreadyHit.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other == myCollider) return;
        if(alreadyHit.Contains(other)) return;

        alreadyHit.Add(other);
 
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage); 
            Debug.Log("Dealt damage: " + damage);
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
