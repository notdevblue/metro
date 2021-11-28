using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform tooltipTextTrm;
    public Text tooltipText;

    public BossHPBar bossHPBar;

    public TMP_Text coinText;

    private CanvasGroup tooltipCG;
    private Vector3 initPosition;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("경고 : 다수의 UI Manager가 실행중입니다.");
        }
        
        instance = this;

        tooltipCG = tooltipTextTrm.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        //초기의 포지션을 저장해두고
        initPosition = tooltipTextTrm.localPosition;
    }

    public static void SetBossHPBar(float value)
    {
        instance.bossHPBar.SetHP(value);
    }

    public static void ShowBossHPBar()
    {
        RectTransform bar = instance.bossHPBar.GetComponent<RectTransform>();
        if(bar != null)
            bar.DOAnchorPosY(-20, 1f);
    }

    public static void HideBossHPBar()
    {
        RectTransform bar = instance.bossHPBar.GetComponent<RectTransform>();
        if (bar != null)
            bar.DOAnchorPosY(bar.rect.height, 1f);
    }

    public static void ShowToolTip(string text)
    {
        instance.tooltipText.text = text;

        Sequence seq = DOTween.Sequence();
        CanvasGroup cg = instance.tooltipCG;
        seq.Append(DOTween.To(() => cg.alpha, value => cg.alpha = value, 1, 0.8f));
        float y = instance.initPosition.y;
        seq.Join(instance.tooltipTextTrm.DOLocalMoveY(y + 120f, 0.5f));
    }

    public static void CloseTooltip()
    {
        DOTween.Clear(); //모든 트윈을 종료시키고 
        //시퀀스를 만들어서 투명도를 다시 투명하게 바꾸고 initPosition으로 보내면 돼
        CanvasGroup cg = instance.tooltipCG;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => cg.alpha, value => cg.alpha = value, 0, 0.8f));
        seq.Join(instance.tooltipTextTrm.DOLocalMoveY(instance.initPosition.y, 0.5f));
    }


    public static void SetCoinText(int count)
    {
        instance.coinText.text = count.ToString();
    }

}
