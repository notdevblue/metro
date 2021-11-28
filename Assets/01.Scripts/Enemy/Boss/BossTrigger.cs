using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public bool isTriggered = false; //활용이 되었는가?
    public int dialogNumber; //몇 번 다이얼로그를 재생할 거니?
    public BossAI targetBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            //카메라 줌아웃 효과를 만들거고
            CamEffectManager.instance.SetCamSize(7, 1f);
            isTriggered = true;
            GameManager.ShowDialog(dialogNumber, EndCallBack);
        }
    }

    private void EndCallBack()
    {
        GameManager.TimeScale = 0;
        //카메라 원래대로 돌려주고
        CamEffectManager.instance.SetCamSize(5, 1f, () =>
        {
            GameManager.TimeScale = 1;
            targetBoss.StartBoss();
        });

        //보스 HP바 보여주고
        UIManager.ShowBossHPBar();
    }
}
