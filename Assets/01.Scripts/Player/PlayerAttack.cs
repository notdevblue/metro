using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격관련")]
    public float timeBetAttack = 0.8f;
    public float lastAttackTime;
    public float attackRange = 1.3f;
    public int attackDamage = 2;
    public LayerMask whatIsEnemy;

    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        lastAttackTime = Time.time;
    }

    void Update()
    {
        if(playerInput.isAttack && Time.time >= lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        playerAnimation.StartAttack();

        Vector2 dir = spriteRenderer.flipX ? transform.right * -1 : transform.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackRange, whatIsEnemy);

        if(hit.collider != null)
        {
                IDamageable iDamage = hit.collider.GetComponent<IDamageable>();

                if(iDamage != null)
                {
                    iDamage.OnDamage(attackDamage, hit.point, hit.normal);
                } 
        }
    }
}
