using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHealth : LivingEntity
{
    public bool isSuperArmor = false;
    public int coinCount = 5;

    BoxCollider2D boxCol;
    Rigidbody2D rigid;
    GoblinAnimation anim;
    EnemyAI ai;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<GoblinAnimation>();
        ai = GetComponent<EnemyAI>();
    }

    private void OnEnable()
    {
        boxCol.enabled = true;
        rigid.gravityScale = 1;
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f)
    {
        if (!isSuperArmor)
        {
            rigid.AddForce(normal * -damage * 2, ForceMode2D.Impulse);
            ai.SetHit();
        }
        // 피격애니메이션을 만들고 싶다면 여기다 넣어야 해
        base.OnDamage(damage, hitPoint, normal);
    }

    protected override void OnDie()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        anim.SetDead();
        boxCol.enabled = false;

        ai.SetDead(); //만들꺼야

        CoinManager.PopCoin(transform.position, coinCount);

        Invoke("DeadProcess", 1f);
    }

    private void DeadProcess()
    {
        gameObject.SetActive(false);
    }

}
