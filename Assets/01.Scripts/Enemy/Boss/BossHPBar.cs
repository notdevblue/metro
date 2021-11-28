using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHPBar : MonoBehaviour
{
    public RectTransform _fillImage;
    public Text _fillText;

    Tween _t = null;

    public void SetHP(float value)
    {
        if(_t != null && _t.IsActive() )
        {
            _t.Kill();
        }
        value = Mathf.Clamp(value, 0f, 1f);
        _t = _fillImage.DOScaleX(value, 0.2f);

        _fillText.text = $"{ (value * 100).ToString("F2")}%";
    }
}
