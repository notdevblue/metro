using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public override void Use(GameObject target)
    {
        GameManager.AddCoin(value);
        base.Use(target);
    }

    public void PopUp(Vector3 pos)
    {
        transform.position = pos;
        float angle = Random.Range(-45f, 45f);
        Vector2 dir = new Vector2(
                        Mathf.Sin(angle * Mathf.Deg2Rad),
                        Mathf.Cos(angle * Mathf.Deg2Rad));
        rigid.AddForce(dir * 10, ForceMode2D.Impulse);
    }

    public void DeployCoin(Vector3 pos)
    {
        transform.position = pos;
    }



}
