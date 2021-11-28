using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : LivingEntity
{
    public Item dropItem;
    public LootBox lootBoxPrefab;

    public int dropCoinCount;

    public int dialogCount = 0;

    private GoblinAnimation _goblinAnim;

    private void Awake()
    {
        _goblinAnim = GetComponent<GoblinAnimation>();
    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal, float power = 0)
    {
        base.OnDamage(damage, hitPoint, normal, power);
        //보스 HP바에 셋팅해줘야 한다.
        UIManager.SetBossHPBar( (float)currentHP / (float)maxHP);
    }

    protected override void OnDie()
    {
        _goblinAnim.SetDead();

        if(dialogCount > 0)
        {
            GameManager.ShowDialog(dialogCount, () =>
            {
                DropAndDeadProcess();
            });
        }
        else
        {
            DropAndDeadProcess();
        }
    }

    private void DropAndDeadProcess()
    {
        //실질적인 드랍이 이루어지도록 한다.
        CoinManager.PopCoin(transform.position, dropCoinCount);

        if(dropItem != null)
        {
            LootBox lb = Instantiate(lootBoxPrefab, transform.position, Quaternion.identity);
            lb.SetLootItem(dropItem);
            lb.Popup(transform.position);

            //여기에 보스의 HP바를 지워주는 걸 해주면 된다.

            UIManager.HideBossHPBar();

            gameObject.SetActive(false);
        }
    }
}
