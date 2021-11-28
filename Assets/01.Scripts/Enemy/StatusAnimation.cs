using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAnimation : MonoBehaviour
{
    private int hashStunBool = Animator.StringToHash("stun");
    private int hashExclaimTrigger = Animator.StringToHash("exclaim");
    private int hashQuestionTrigger = Animator.StringToHash("question");

    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayStun(float stunTime)
    {
        StartCoroutine(StunRecover(stunTime));
    }

    IEnumerator StunRecover(float time)
    {
        _anim.SetBool(hashStunBool, true);
        yield return new WaitForSeconds(time - 0.2f);
        _anim.SetBool(hashStunBool, false);
    }

    public void PlayExclaim()
    {
        _anim.SetTrigger(hashExclaimTrigger);
    }

    public void PlayQuestion()
    {
        _anim.SetTrigger(hashQuestionTrigger);
    }
}
