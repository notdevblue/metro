using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyMove))] // 자동으로 ()안의 컴포넌트 붙여줌, 널체크 방지용 
public class EnemyFOV : MonoBehaviour
{
    public float viewRange = 10f; //시야거리
    [Range(0,360)]
    public float viewAngle = 40f; //시야각도
    public float attackRange = 1f; //공격거리

    public LayerMask obstacleLayer; //장애물 감지 
    private int playerLayer;

    private EnemyMove enemyMove; // 적 현재 진행방향을 알기우한 코ㅓㅁ포넌트

    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
        playerLayer = LayerMask.NameToLayer("PLAYER"); //레이어 숫자 알려줌
    }

    public Vector2 CirclePoint(float angle)
    {
        if(enemyMove != null)
        {
            angle += enemyMove.GetFront().x < 0 ? -90f : 90f;
        }
        else
        {
            angle += 90f;
        }
        
        return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool IsTracePlayer()
    {
        bool isTrace = false;
        Collider2D col = Physics2D.OverlapCircle(transform.position, viewAngle, 1 << playerLayer);
        
        if(col != null && enemyMove != null)
        {
            // z축 필요없으니 벡터 2로 변환시킴
            Vector2 dir = GameManager.Player.position - transform.position;

            if (Vector2.Angle(enemyMove.GetFront(), dir) < viewAngle * 0.5f)
            {

                isTrace = true;
            }
        }
       
        return isTrace;
    }

    public bool IsViewPlayer()
    {
        bool isView = false;
        Vector2 dir = GameManager.Player.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast
            (transform.position, dir.normalized, viewRange, obstacleLayer);

        if(hit2D.collider != null)
        {
            isView = (hit2D.collider.gameObject.CompareTag("Player"));
        }

        return isView;
    }

    public bool IsAttackPossible()
    {
        return (GameManager.Player.position - transform.position).sqrMagnitude
            <= Mathf.Pow(attackRange, 2);
    }


}
