using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform tooltipTextTrm;
    public Text toolTipText;

    private CanvasGroup tooltipCG;
    private Vector3 initPosition;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Warn > UIManager are running more than one in same scene");
        }
        instance = this;

        tooltipCG = tooltipTextTrm.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        initPosition = tooltipTextTrm.localPosition;   
    }

    static public void ShowToolTip(string text)
    {
        instance.toolTipText.text = text;

        Sequence seq = DOTween.Sequence();
        CanvasGroup cg = instance.tooltipCG;

        seq.Append(DOTween.To(() => cg.alpha, value => cg.alpha = value, 1, 0.8f));
        float y = instance.initPosition.y;

        seq.Join(instance.tooltipTextTrm.DOLocalMoveY(y + 150.0f, 1.0f));
    }

    static public void CloseToolTip()
    {
        DOTween.Clear(); // 모든 Tween 을 종료시키고

        Sequence seq = DOTween.Sequence();
        CanvasGroup cg = instance.tooltipCG;

        seq.Append(DOTween.To(() => cg.alpha, value => cg.alpha = value, 0.0f, 0.8f));

        seq.Join(instance.tooltipTextTrm.DOLocalMoveY(instance.initPosition.y, 1.0f));
    }



}
