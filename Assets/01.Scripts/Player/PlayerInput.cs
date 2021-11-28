using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

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

    public PlayerMode mode;

    private void Start()
    {
        mode = PlayerMode.Idle;
    }

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

        switch(mode)
        {
            case PlayerMode.Attack:
                xMove = 0;
                isAttack = Input.GetButtonDown("Fire1");
                break;
            case PlayerMode.Hit:
            case PlayerMode.Idle:
            case PlayerMode.Jumping:
            case PlayerMode.Moving:
                xMove = Input.GetAxisRaw("Horizontal");
                isJump = Input.GetButtonDown("Jump");
                isDash = Input.GetButtonDown("Dash");
                isAttack = Input.GetButtonDown("Fire1");
                isUse = Input.GetButtonDown("Use"); //당연히 이건 아직 없다 r키로 할당해줄 예정
                break;
        }


        
    }
}
