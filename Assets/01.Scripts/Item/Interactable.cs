using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IUseAble
{
    public string useText;

    public abstract void Use(GameObject target);

    protected bool _used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_used) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            //UIManager를 통해서 툴팁 텍스트를 보여주도록하고
            UIManager.ShowToolTip(useText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //UIManager를 통해서 툴팁 텍스트를 사라지도록 한다
            UIManager.CloseTooltip();
        }
    }
}
