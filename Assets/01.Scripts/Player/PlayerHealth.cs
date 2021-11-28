using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public float damageDelay;
    public float recoverDelay = 0.5f;
    
    private PlayerMove playerMove;
    private float lastDamageTime;

    private void Awake()
    {
        lastDamageTime = Time.time;
        playerMove = GetComponent<PlayerMove>();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power)
    {
        if (lastDamageTime + damageDelay > Time.time) return; //연속 데미지는 막아주고

        lastDamageTime = Time.time;

        //나중에 여기서 데미지 처리 로직을 만들어봅시다.
        base.OnDamage(damage, hitPoint, normal, power);

        playerMove.SetHit(normal, power, recoverDelay);
    }

    protected override void OnDie()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if(item)
        {
            item.Use(gameObject); //플레이어를 보내준다.
        }
    }
}
