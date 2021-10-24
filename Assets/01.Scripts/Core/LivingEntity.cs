using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    public int maxHP;
    protected int currentHP;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            OnDie();
        }
    }

    protected abstract void OnDie();

}

