using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IUseAble
{
    public int value;

    public virtual void Use(GameObject target)
    {
        gameObject.SetActive(false);
    }
}
