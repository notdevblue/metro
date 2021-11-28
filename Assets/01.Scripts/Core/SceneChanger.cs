using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    public RectTransform backImage;
    private RectTransform backImageParent;
    private CanvasGroup backImageCg;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 씬체인저가 구동중입니다.");
        }
        instance = this;
    }

    void Start()
    {
        backImageParent = backImage.parent as RectTransform;
        backImageCg = backImage.GetComponent<CanvasGroup>();
        backImage.sizeDelta = backImageParent.sizeDelta;
        backImage.anchoredPosition = new Vector3(backImageParent.sizeDelta.x, 0, 0);
    }

    public void ChangeSection(Link source, Link target)
    {
        backImageCg.alpha = 0;
        GameManager.TimeScale = 0;

        Sequence seq = DOTween.Sequence();
        seq.Append(backImage.DOAnchorPosX(0, 1f));
        seq.Join(DOTween.To(
            () => backImageCg.alpha,
            value => backImageCg.alpha = value, 1, 1f));
        //여기까지가 오른쪽에 있던 까만화면이 덮어지는 부분이야

        seq.AppendCallback(() =>
        {
            source.SetActiveSection(false);
            target.SetActiveSection(true);
            GameManager.Player.position = target.transform.position;
            CamEffectManager.instance.SetCamBound(target.camBound);//옮기는 것으로 바운드 설정
        });

        seq.AppendInterval(1f); //대기하는 거
        seq.Append(backImage.DOAnchorPosX(backImage.sizeDelta.x, 1f));
        seq.Join(DOTween.To(
            () => backImageCg.alpha,
            value => backImageCg.alpha = value, 0, 1f));

        seq.AppendCallback(() =>
        {
            GameManager.TimeScale = 1f;
        });
    }
}