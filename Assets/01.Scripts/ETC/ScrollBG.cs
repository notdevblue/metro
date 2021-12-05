using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public float ratio;

    private Transform camTransform;
    private float beforeX;

    private void Start()
    {
        camTransform = Camera.main.transform;
        beforeX = camTransform.position.x;
    }

    private void FixedUpdate()
    {
        float delta = camTransform.position.x - beforeX;
        Vector3 nextPos = transform.position;
        
        nextPos.x += delta * ratio;

        transform.position = nextPos;

        beforeX = camTransform.position.x;
    }

    public void ResetBG()
    {
        beforeX = camTransform.position.x;
        transform.localPosition = Vector3.zero;
    }
}
