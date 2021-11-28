using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoot : UniqueItem
{
    public override void Use(GameObject target)
    {
        if (!_canUse || _used) return;

        _used = true;

        PlayerMove move = target.GetComponent<PlayerMove>();
        if(move != null)
        {
            move.jumpCount += 1;
            GameManager.ShowDialog(_dialogNumber);
            base.Use(target);
        }        
    }
}
