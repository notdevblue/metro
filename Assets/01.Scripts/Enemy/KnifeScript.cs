using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public LayerMask whatIsEnemy;

    public int damage;
    private bool isAttack = false;

    public void SetAttack(bool value)
    {
        isAttack = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttack) return;

        if (( 1 << collision.gameObject.layer & whatIsEnemy) > 0)
        {
            IDamageable hp = collision.gameObject.GetComponent<IDamageable>();

            if (hp != null)
            {
                ContactPoint2D contact = collision.contacts[0];
                     
                hp.OnDamage(damage, contact.point, contact.normal);
            }

        }
    }

}
