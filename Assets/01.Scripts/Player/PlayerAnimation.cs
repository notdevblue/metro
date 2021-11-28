using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{    
    private Animator anim;
    private Rigidbody2D rigid;
    private PlayerMove playerMove;
    
    private readonly int hashXMove = Animator.StringToHash("xMove");
    private readonly int hashYSpeed = Animator.StringToHash("ySpeed");
    private readonly int hashIsGround = Animator.StringToHash("isGround");
    private readonly int hashIsJumping = Animator.StringToHash("isJumping");
    private readonly int hashDoubleJump = Animator.StringToHash("doubleJump");
    private readonly int hashAttack = Animator.StringToHash("attack");
    private readonly int hashIsHit = Animator.StringToHash("isHit");
    private readonly int hashHit = Animator.StringToHash("hit");

    private bool jumping = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        //immutable , mutable
        anim.SetFloat(hashXMove, Mathf.Abs(rigid.velocity.x ));
        anim.SetFloat(hashYSpeed, rigid.velocity.y); //y속도 넣기
        anim.SetBool(hashIsGround, playerMove.isGround); //땅바닥 상태
    }

    ///<summary>
    /// 플레이어 점핑 애니메이션 재생 매서드
    ///</summary>
    public void Jump()
    {
        if(jumping){
            anim.SetTrigger(hashDoubleJump);
        }else{
            anim.SetBool(hashIsJumping, true);
        }
        jumping = true;
    }

    ///<summary>
    /// 플레이어 점핑 애니메이션 종료 매서드
    ///</summary>
    public void JumpEnd()
    {
        anim.SetBool(hashIsJumping, false);
        jumping = false;
    }

    public void StartAttack()
    {
        anim.SetTrigger(hashAttack);
    }

    public void SetHit(bool value)
    {
        anim.SetBool(hashIsHit, value);
        if (value)
        {
            anim.SetTrigger(hashHit);
        }
    }
}