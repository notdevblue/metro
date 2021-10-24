using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    private Animator animator;
    private readonly int hashIsDead = Animator.StringToHash("isDead");
    private readonly int hashDie = Animator.StringToHash("dead");
    private readonly int hashHit = Animator.StringToHash("hit");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDead()
    {
        animator.SetBool(hashIsDead, true);
        animator.SetTrigger(hashDie);
    }

    public void SetHit()
    {
        animator.SetTrigger(hashHit);
    }


}
