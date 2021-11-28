using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public override void Use(GameObject target)
    {
        GameManager.AddCoin(value);
        base.Use(target);
    }

    public void DeployCoin(Vector3 pos)
    {
        transform.position = pos;
    }
}
