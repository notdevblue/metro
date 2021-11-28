using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : EnemyAttack
{
    protected KnifeScript knife;
    protected GoblinAnimation anim;

    protected override void Awake()
    {
        knife = GetComponentInChildren<KnifeScript>();
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

    //플레이어가 고블린과 몸통박치기 할 경우 . 맞아야지
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if( (1 << collision.gameObject.layer & knife.whatIsEnemy) > 0)
        {
            //충돌한 녀석에게서 IDamageable이 있는지 체크해서
            IDamageable hp = collision.gameObject.GetComponent<IDamageable>();

            //그녀석이 null이 아니면 
            if (hp != null)
            {
                ContactPoint2D cp2 = collision.contacts[0];
                Vector2 normal = cp2.normal;
                if (Mathf.Abs(normal.x) < 0.1f)
                    normal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;
                
                hp.OnDamage(knife.damage, cp2.point, normal, 5);
            }
            // 충돌한 녀석의 contacts[0]를 가져와서 point와, normal을 뽑아내고
            //그걸로 데미지를 가한다.
        }
    }
}
