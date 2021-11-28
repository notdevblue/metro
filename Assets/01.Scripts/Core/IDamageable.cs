using UnityEngine;

public interface IDamageable 
{
    void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f);
}
