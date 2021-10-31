using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttack : MonoBehaviour
{
    public float attackDelay;
    protected float lastAttackTime = 0;
    public bool isAttack = false;

    protected virtual void Awake()
    {
        lastAttackTime = Time.time;
    }

    public abstract void Attack();

    private void Update()
    {
        if (isAttack)
        {
            if (lastAttackTime + attackDelay <= Time.time)
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
    }

    

}
