using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    int health;
    public event Action OnTakeDamge;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) return;
        
        // 둘 중에 큰 수 반환
        health = Mathf.Max(health - damage, 0);

        OnTakeDamge?.Invoke();
        Debug.Log("Health: " + health);
    }

}
