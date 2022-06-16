using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{   
    [SerializeField] Collider myCollider;
    List<Collider> alreadyHit = new List<Collider>();

    PlayerStateMachine statemachine;
    int damage;
    float knockback;

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

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forcereciver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forcereciver.AddForce( direction* knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
