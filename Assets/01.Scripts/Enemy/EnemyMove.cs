using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyMove : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    public bool factingRight = true; // 오른쪽 보고 있는감

    public float judgeDistance = 0.1f;
   // public float patrolDistance = 1.5f;
    public float currentSpeed;

    public float chaseSpeed = 2f;
    public float moveSpeed = 1f;

    protected bool isChase = false;

    protected Vector2 destination;
    protected bool moveSet = false;
    protected Rigidbody2D rigid;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public Vector2 GetFront()
    {
        if (factingRight)
        {
            return spriteRenderer.flipX ? transform.right * -1 : transform.right;
        }
        else
        {
            return spriteRenderer.flipX ? transform.right : transform.right * -1;
        }
    }

    public virtual void Stop()
    {
        moveSet = false;
    }

    public virtual void SetChase(Vector2 target)
    {
        destination = target;
        currentSpeed = chaseSpeed;
        isChase = true;
        moveSet = true;
    }

    public virtual void SetMove()
    {
        isChase = false;
        moveSet = true;
        currentSpeed = moveSpeed;
    }
    
    public void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    
}
