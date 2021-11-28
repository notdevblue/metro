using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyAI
{
    private void Start()
    {
        currentState = State.Idle;
        move.Stop();
    }

    //보스의 동작을 실행하는 거
    public void StartBoss()
    {
        currentState = State.Chase;
        //enemyAttack 을 활성화시켜서 공격시작해야함.
        attack.OnStartAttack();
    }

    protected override void CheckState()
    {
        if (currentState == State.Hit || currentState == State.Dead || currentState == State.Idle
            || currentState == State.Skill || currentState == State.Stun) return;

        bool isAttack = fov.IsAttackPossible();
        bool isTrace = fov.IsTracePlayer();
        
        if(isAttack && isTrace)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Chase;
        }
    }

    protected override void Action()
    {
        switch(currentState )
        {
            case State.Chase:
                //공격 비활성화
                if (attack != null) attack.isAttack = false;
                move.SetChase(GameManager.Player.position);
                break;
            case State.Attack:
                move.Stop();
                //공격활성화
                if (attack != null) attack.isAttack = true;
                break;
        }
    }
}
