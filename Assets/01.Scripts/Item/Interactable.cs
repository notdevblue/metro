using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour, IUseAble
{
    const string PLAYER = "Player";
    public string useText;

    abstract public void Use(GameObject target);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag(PLAYER))
        {
            UIManager.ShowToolTip(useText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag(PLAYER))
        {
            UIManager.CloseToolTip();
        }
    }
}
