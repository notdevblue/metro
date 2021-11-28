using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

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
    private PlayerMove playerMove;

    public float keyDelay = 0.2f;
    private int attackMode = 0;
    private float currentKeyDelay = 0.0f;
    private bool canAttack = false; // 연타모드
    public float attackSpeed = 1.2f;


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMove = GetComponent<PlayerMove>();
    }
    
    void Start()
    {
        lastAttackTime = Time.time;
    }

    void Update()
    {
        // 첫타
        if(playerInput.mode != PlayerMode.Attack && playerInput.isAttack && Time.time >= lastAttackTime + timeBetAttack)
        {
            playerAnimation.SetAnimationSpeed(attackSpeed);
            Attack();
        }
        
        if (playerInput.mode == PlayerMode.Attack && canAttack)
        {
            currentKeyDelay -= Time.deltaTime;
            
            if(currentKeyDelay <= 0)
            {
                currentKeyDelay = 0;
                attackMode = 0;
                lastAttackTime = Time.time;
                playerAnimation.StopAttack();
                playerInput.mode = PlayerMode.Idle;
            }
            else if (playerInput.isAttack)
            {
                Attack();
                canAttack = false;
            }
        }
    }

    public void MotionEnd()
    {
        canAttack = true;
        currentKeyDelay = keyDelay;
    }

    public void CheckDamage()
    {
        Vector2 dir = playerMove.GetFront();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackRange, whatIsEnemy);

        if (hit.collider != null)
        {
            IDamageable iDamage = hit.collider.GetComponent<IDamageable>();

            if (iDamage != null)
            {
                iDamage.OnDamage(attackDamage, hit.point, hit.normal);
            }
        }
    }

    private void Attack()
    {
        playerInput.mode = PlayerMode.Attack;
        ++attackMode;
        if(attackMode >= 4) attackMode = 1;

        canAttack = false;
        playerAnimation.StartAttack(attackMode);


    }
}
