using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    ///<summary>   ///xml 주석
    ///사용자의 x축 입력값
    ///</summary>
    public float xMove { get; private set; }
    public bool isJump { get; private set; }
    public bool isDash { get; private set; }
    public bool isAttack { get; private set; }
    public bool isUse { get; private set; }

    void Update()
    {
        //Axis 조이스틱을 기반으로 0 ~ 1
        //Raw 키보드 입력 기반 0, 1
        if(GameManager.TimeScale <= 0)
        {
            xMove = 0;
            isJump = false;
            isDash = false;
            isAttack = false;
            isUse = false;
            return;
        }
        xMove = Input.GetAxisRaw("Horizontal");
        isJump = Input.GetButtonDown("Jump");
        isDash = Input.GetButtonDown("Dash");
        isAttack = Input.GetButtonDown("Fire1");
        isUse = Input.GetButtonDown("Use");
    }
}
