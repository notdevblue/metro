using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : Interactable
{
    public int dialogCode; //몇번 다이얼로그를 재생할지

    private bool isInteract = false;

    public override void Use(GameObject target)
    {
        if (isInteract) return;
        
        isInteract = true;
        GameManager.ShowDialog(dialogCode, ()=>
        {
            isInteract = false;
        });
    }    
}
