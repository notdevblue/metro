using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : EnemyAttack
{
    private Knife knife;
    protected GoblinAnimation anim;

    protected override void Awake()
    {
        base.Awake();
        knife = GetComponentInChildren<Knife>();
        anim = GetComponent<GoblinAnimation>();
    }

    public override void Attack()
    {
        knife.SetAttack(true);
        anim.PlayAttack();
    }

    public void SetAttackDisable()
    {
        knife.SetAttack(false);
    }

    // 몸통박치기
    private void OnCollisionEnter2D(Collision2D other)
    {
        if((1 << other.gameObject.layer & knife.whatIsEnemy) > 0)
        {
            IDamageable hp = other.transform.GetComponent<IDamageable>();

            if(hp != null)
            {
                ContactPoint2D cp2 = other.contacts[0];
                Vector2 normal = cp2.normal;

                if(Mathf.Abs(normal.x) < 0.1f)
                {
                    normal.x = other.transform.position.x < transform.position.x ? 1 : -1;

                }
                hp.OnDamage(knife.damage, cp2.point, normal, 5.0f);
            }
        }
    }
}
