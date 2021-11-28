using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public float attackDealy;

    protected float lastAttackTime = 0;
    public bool isAttack = false;

    protected virtual void Awake()
    {
        lastAttackTime = Time.time;
    }

    public abstract void Attack();
    
    protected virtual void Update()
    {
        if(isAttack)
        {
            if(lastAttackTime + attackDealy <= Time.time) //딜레이가 끝나 다시 공격가능하다면
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
    }

    public virtual void OnStartAttack()
    {
        //Do nothing;
    }
}
