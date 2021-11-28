using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IUseAble
{
    public int value;
    protected Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void PopUp(Vector3 pos)
    {
        transform.position = pos;
        float angle = Random.Range(-45f, 45f);
        Vector2 dir = new Vector2(
                        Mathf.Sin(angle * Mathf.Deg2Rad),
                        Mathf.Cos(angle * Mathf.Deg2Rad));
        _rigid.AddForce(dir * 10, ForceMode2D.Impulse);
    }

    public virtual void Use(GameObject target)
    {
        gameObject.SetActive(false);
    }
}
