using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniqueItem : Item
{
    public int _dialogNumber = 5;
    protected bool _canUse = false;
    protected bool _used = false;

    void Start()
    {
        _canUse = false;
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(1f);
        _canUse = true;
    }
}
