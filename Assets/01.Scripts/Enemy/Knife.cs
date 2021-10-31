using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public LayerMask whatIsEnemy;

    public int damage;
    private bool isAttack = false;

    public void SetAttack(bool value)
    {
        isAttack = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isAttack) return;

        if((1 << other.gameObject.layer & whatIsEnemy) > 0) // layer = 5번 레이어라 1 << layer 무튼그럼
        {
            IDamageable hp = other.gameObject.GetComponent<IDamageable>();

            if(hp != null)
            {
                ContactPoint2D contact = other.contacts[0];

                hp.OnDamage(damage, contact.point, contact.normal); // contacts[index].point 충돌한 지점의 point
            }
        }
    }
}
