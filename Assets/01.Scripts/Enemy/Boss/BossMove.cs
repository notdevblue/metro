using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : EnemyMove
{
    public bool start = false; //시작전에는 보스가 멈춰있다.

    public override void SetChase(Vector2 target)
    {
        base.SetChase(target);
    }

    public void SetForce(Vector2 power)
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(power, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if(moveSet)
        {
            Vector2 moveDir = destination - (Vector2)transform.position;

            rigid.velocity = new Vector2(
                moveDir.normalized.x * currentSpeed * GameManager.TimeScale, 
                rigid.velocity.y);

            if (facingRight)
            {
                if (moveDir.x > 0 && transform.localScale.x < 0 
                    || moveDir.x < 0 && transform.localScale.x > 0) Flip();
            }
            else
            {
                if (moveDir.x < 0 && transform.localScale.x < 0 
                    || moveDir.x > 0 && transform.localScale.x > 0) Flip();
            }
        }
    }
}
