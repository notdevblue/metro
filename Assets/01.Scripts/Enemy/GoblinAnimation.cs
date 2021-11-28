using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;

    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashAttack = Animator.StringToHash("attack");
    private readonly int hashDie = Animator.StringToHash("die");
    private readonly int hashDead = Animator.StringToHash("dead");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float xSpeed = Mathf.Abs(rigid.velocity.x);
        animator.SetFloat(hashSpeed, xSpeed);
    }

    public void PlayAttack()
    {
        animator.SetTrigger(hashAttack);
    }

    public void SetDead()
    {
        animator.SetBool(hashDead, true);
        animator.SetTrigger(hashDie);
    }
    

}
