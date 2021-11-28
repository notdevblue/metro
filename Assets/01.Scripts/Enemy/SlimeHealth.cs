using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : LivingEntity
{
    public int coinCoint = 3; // 코인 떨구는 갯수

    BoxCollider2D boxCollider2D;
    Rigidbody2D rigid;
    SlimeAnimation slimeAnim;
    EnemyAI ai;

    private void Awake()
    {
        ai = GetComponent<EnemyAI>();
        rigid = GetComponent<Rigidbody2D>();
        slimeAnim = GetComponent<SlimeAnimation>();
        boxCollider2D = GetComponent<BoxCollider2D>();     
    }

    private void OnEnable()
    {
        boxCollider2D.enabled = true;
        rigid.gravityScale = 1f;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f)
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(normal * -damage * 2 + new Vector2(0,3f) , ForceMode2D.Impulse);
        slimeAnim.SetHit();
        base.OnDamage(damage, hitPoint, normal);

        ai.SetHit(); // 일시적으로 ai 정지 및 일정시간후 다시 돌아옴 

    }

    protected override void OnDie()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;

        slimeAnim.SetDead();

        boxCollider2D.enabled = false;

        CoinManager.PopCoin(transform.position, coinCoint);


        Invoke("DeadProcess", 1f);
    }

    private void DeadProcess()
    {
        gameObject.SetActive(false);
    }


}
