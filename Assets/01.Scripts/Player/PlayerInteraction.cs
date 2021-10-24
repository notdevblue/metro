using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private PlayerInput playerInput;
    private Interactable thing = null;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }


    private void Update()
    {
        if(playerInput.isUse && thing != null)
        {
            thing.Use(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable i = other.transform.GetComponent<Interactable>();
        if (i != null)
        {
            thing = i;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable i = other.transform.GetComponent<Interactable>();
        if(i != null)
        {
            thing = null;
        }
    }
}
