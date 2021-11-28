using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Interactable
{
    public Item dropItem;

    private readonly int hashOpenTrigger = Animator.StringToHash("open");
    private BoxCollider2D _boxCol;
    private Animator _anim;
    private Rigidbody2D _rigid;

    private bool _canUse = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _boxCol = GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _canUse = false;
    }

    //private void Start()
    //{
    //    StartCoroutine(Test());
    //}

    //IEnumerator Test()
    //{
    //    yield return new WaitForSeconds(2f);
    //    Popup(transform.position);
    //}

    public void SetLootItem(Item item)
    {
        dropItem = item;
    }

    public override void Use(GameObject target)
    {
        if (!_canUse || _used) return;

        _used = true;
        _anim.SetTrigger(hashOpenTrigger);
        Item item = Instantiate(dropItem, transform.position, Quaternion.identity);

        item.PopUp(transform.position);
    }

    public void Popup(Vector3 pos)
    {
        transform.position = pos;
        _rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(1f);
        _rigid.gravityScale = 0f;
        _boxCol.isTrigger = true;
        _rigid.velocity = Vector2.zero;
        _canUse = true;
    }
}
