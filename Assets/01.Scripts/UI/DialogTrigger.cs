using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public int code;
    private bool isRead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRead) return;

        if(collision.gameObject.CompareTag("Player"))
        {
            isRead = true;
            GameManager.ShowDialog(code);
        }
    }
}
