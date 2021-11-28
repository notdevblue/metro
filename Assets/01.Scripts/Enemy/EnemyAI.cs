using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Hit,
        Skill,  //스킬 사용 상태
        Stun,  //스턴당한 상태
        Dead
    }
    public State currentState = State.Idle;
    public float judgeTime = 0.2f;
    public float stunTime = 0.5f;

    public StatusAnimation _status;

    private WaitForSeconds ws;
    protected EnemyMove move;
    protected EnemyFOV fov;
    protected EnemyAttack attack;

    private void Awake()
    {
        ws = new WaitForSeconds(judgeTime);
        fov = GetComponent<EnemyFOV>();
        move = GetComponent<EnemyMove>();
        attack = GetComponent<EnemyAttack>(); //이렇게 하면 자기한테 맞는 Attack 이 가져와져
        _status = GetComponentInChildren<StatusAnimation>(); //자식에 달려있는 상태 
    }

    private void OnEnable()
    {
        StartCoroutine(CheckNextAction());
    }

    IEnumerator CheckNextAction()
    {
        while (true)
        {
            if(GameManager.Player != null) // 안전빵 코드 
            {
                // ?? ㅋㅋㅋ 상태체크 , 상태따라 액션

                CheckState();

                Action();
            }
            yield return ws;
        }
    }

    protected virtual void CheckState()
    {
        if(currentState == State.Hit || currentState == State.Dead || currentState == State.Stun)
        {
            return;
        }

        bool isTrace = fov.IsTracePlayer();
        bool isView = fov.IsViewPlayer();
        bool isAttack = fov.IsAttackPossible();

        if(isAttack && isView && isTrace)
        {
            currentState = State.Attack;
        }
        else if(isTrace && isView)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }

    }
    protected virtual void Action()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                if (attack != null)
                    attack.isAttack = false;
                move.SetMove();
                break;
            case State.Chase:
                if (attack != null)
                    attack.isAttack = false;
                move.SetChase(GameManager.Player.position);
                break;
            case State.Attack:
                move.Stop();
                if (attack != null)
                    attack.isAttack = true;
                break;
            case State.Hit:
                move.Stop();
                if (attack != null)
                    attack.isAttack = false;
                break;
            case State.Dead:
                break;           
        }
    }

    public void SetHit()
    {
        currentState = State.Hit;
        move.Stop();
        StartCoroutine(Recover(stunTime));
    }

    public void SetStun(float time = 0)
    {
        currentState = State.Stun;
        if (time == 0)
            time = stunTime;

        if(_status != null)
            _status.PlayStun(time);

        //여기에 스턴 애니메이션 사용
        StartCoroutine(Recover(time));
    }

    private IEnumerator Recover( float time)
    {
        yield return new WaitForSeconds(time);

        currentState = State.Patrol;
    }

    public void SetDead()
    {
        currentState = State.Dead;
        if (attack != null)
            attack.isAttack = false;
        move.Stop();
    }
}
     