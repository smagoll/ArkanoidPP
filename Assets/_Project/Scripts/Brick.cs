using UnityEngine;
using System;

public class Brick : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp;
    
    public event Action<Brick> OnDestroyed;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}