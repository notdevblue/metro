using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMove : EnemyMove
{
    public LayerMask whatIsGround;
    private Vector2 moveDir;

    private Vector3 spriteSize;

    private void Start()
    {
        moveDir = transform.right;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteSize = sr.bounds.size;
    }

    public override void SetMove()
    {
        if (isChase)
        {
            moveDir = transform.right;
        }
        base.SetMove();
       
    }

    private void FixedUpdate()
    {
        if (moveSet)
        {
            if (isChase)
            {
                moveDir = (destination - (Vector2)transform.position).normalized; // z축 날리려고 변환 
            }

            rigid.velocity = new Vector2(moveDir.x * currentSpeed * GameManager.TimeScale, rigid.velocity.y);

            if (facingRight)
            {
                if (moveDir.x > 0 && transform.localScale.x < 0 || moveDir.x  < 0 && transform.localScale.x > 0)
                {
                    Flip();
                }
            }
            else
            {
                if (moveDir.x < 0 && transform.localScale.x < 0 || moveDir.x > 0 && transform.localScale.x > 0)
                {
                    Flip();
                }
            }

            if (!CheckGround())
            {

                if(isChase)
                {
                    Stop();
                }
                else
                {
                    moveDir *= -1;
                }
                
            }
        }
    }

    private bool CheckGround()
    {   
        Vector2 pos = transform.position;
        Vector2 frontDownPosition = new Vector2(pos.x + moveDir.x * 0.35f, pos.y - spriteSize.y / 2 );
        Vector2 frontPosition = new Vector2(pos.x + moveDir.x * (spriteSize.x / 2 + 0.35f), pos.y);

        return Physics2D.OverlapCircle(frontDownPosition, 0.2f, whatIsGround)
            && !Physics2D.OverlapCircle(frontPosition, 0.2f, whatIsGround);

    }


    // ㄱㄱ 
}
