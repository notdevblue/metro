using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    public int maxHP;
    protected int currentHP;
    public Color hitColor;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 0)
    {
        // 피격 파티클은 여기서 재생 
        BloodParticle hitParticle = PoolManager.GetItem<BloodParticle>();
        hitParticle.SetParticleColor(hitColor);
        hitParticle.SetRotation(normal);
        hitParticle.Play(hitPoint);

        currentHP -= damage;
        if (currentHP <= 0)
        {
            OnDie();
        }
    }

    protected abstract void OnDie();

}

