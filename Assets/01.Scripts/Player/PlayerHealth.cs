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


    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {
        base.OnDamage(damage, hitPoint, normal);
    }

    protected override void OnDie()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Item item = other.transform.GetComponent<Item>();
        item?.Use(gameObject);
    }
}
