using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

public class CamEffectManager : MonoBehaviour
{
    public static CamEffectManager instance;

    public CinemachineVirtualCamera vCam;


    CinemachineConfiner confiner;
    CinemachineBasicMultiChannelPerlin cmPerlin;

    Tween camTween = null;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 카메라 이펙터가 실행중입니다.");
        }
        instance = this;
    }

    private void Start()
    {
        confiner = vCam.gameObject.GetComponent<CinemachineConfiner>();
        cmPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetCamSize(float size, float duration = 1f, Action callBack = null)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(DOTween.To(
            () => vCam.m_Lens.OrthographicSize,
            value => vCam.m_Lens.OrthographicSize = value,
            size, duration ));

        seq.AppendCallback(() =>
        {
            if (callBack != null) callBack();
        });
    }

    public void SetCamBound(Collider2D collider)
    {
        confiner.m_BoundingShape2D = collider;
    }

    public void SetCamShake(float duration, float power = 5f)
    {
        if(camTween != null && camTween.IsActive())
        {
            camTween.Kill();
        }
        cmPerlin.m_AmplitudeGain = power;
        camTween = DOTween.To(
            () => cmPerlin.m_AmplitudeGain,
            value => cmPerlin.m_AmplitudeGain = value,
            0, duration);
    }
}
