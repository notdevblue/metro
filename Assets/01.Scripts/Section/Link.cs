using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public int linkId;
    public Link targetLink;
    public Collider2D camBound;


    private Section parentSection;

    private void Start()
    {
        parentSection = GetComponentInParent<Section>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SceneChanger.instance.ChangeSection(this, targetLink);
        }
    }


    public void SetActiveSection(bool value)
    {
        parentSection.gameObject.SetActive(value);
    }
}
