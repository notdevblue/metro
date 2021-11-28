using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossAttack : GoblinAttack
{
    public float farDistance = 5f; //이거 이상 멀어지면 돌진밖는다.
    public float dashAttackDelay = 8f; //대가리 밖는 공격의 딜레이 시간
    private float lastDashAttackTime = 0; //마지막으로 대가리 밖았던 시간

    //텍스트가 출력될 수 있는 걸 가지고 있어야 해
    public ShortText shortText; 

    private EnemyAI ai;
    public bool isPattern = false; //특수공격 패턴중임을 알 수 있는 변수
    private BossMove move;  //보스의 무브 스크립트

    public LayerMask whatIsWall; // 뭐가 벽인지 알아야 때려박고 기절한다.

    protected override void Awake()
    {
        base.Awake();
        ai = GetComponent<EnemyAI>();
        move = GetComponent<BossMove>();
    }

    public override void OnStartAttack()
    {
        lastDashAttackTime = Time.time; //시작하면 대시어택 시간 초기화
    }

    protected override void Update()
    {
        if (ai.currentState == EnemyAI.State.Idle) return; //대기상태는 아무것도 안함

        Vector2 distanceVector = GameManager.Player.position - transform.position;
        if(!isPattern && distanceVector.sqrMagnitude >= farDistance * farDistance 
            && lastDashAttackTime + dashAttackDelay <= Time.time)
        {
            isPattern = true;
            ai.currentState = EnemyAI.State.Skill;
            move.Stop();
            StartCoroutine(DashSequence());
        }else if(!isPattern)
        {
            base.Update(); //가까이 있고 패턴수행 아니면 푹찍푹찍! 
        }
    }
    //돌진 시퀀스
    IEnumerator DashSequence()
    {
        //도망치지 마라는 텍스트를 출력하고
        shortText.ShowText("도망치지 마라!", 1f);
        //여기서 플레이어의 방향을 확인하고 돌진
        Vector2 dir = new Vector2(GameManager.Player.position.x - transform.position.x, 0);
        yield return new WaitForSeconds(1.2f);

        move.SetForce(dir.normalized * 20);

        yield return new WaitForSeconds(2f);
        // 내가 벽에 때려박아서 스턴이 걸렸거나, 또는 플레이어를 박아서 데미지를 줬거나
        if(ai.currentState != EnemyAI.State.Stun)
        {
            ai.currentState = EnemyAI.State.Chase; //스턴걸리지 않았다면 추적상태로 변경
        }
        lastDashAttackTime = Time.time;
        isPattern = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPattern)
        {
            base.OnCollisionEnter2D(collision);
        }
        else
        {
            if( (1 << collision.gameObject.layer & knife.whatIsEnemy) > 0  )
            {
                LivingEntity le = collision.gameObject.GetComponent<LivingEntity>();
                ContactPoint2D cp2d = collision.contacts[0];
                Vector2 cpNormal = cp2d.normal;

                if(Mathf.Abs(cpNormal.x) < 0.1f)
                {
                    cpNormal.x = collision.gameObject.transform.position.x < transform.position.x ? 1 : -1;
                }
                if (le != null)
                    le.OnDamage(knife.damage * 3, cp2d.point, cpNormal, 8);
            }
            else if( (1 << collision.gameObject.layer & whatIsWall ) > 0)
            {
                ai.SetStun(); //벽에 부딛히면 스턴처리
            }
        }
        
    }
}
