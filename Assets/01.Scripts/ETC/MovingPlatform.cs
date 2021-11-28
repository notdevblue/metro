using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float distance = 3f;
    public float direction = -1f;
    public float speed = 2f;

    private float movedDistance = 0;

    // Update is called once per frame
    void Update()
    {
        float move = speed * direction * Time.deltaTime * GameManager.TimeScale;
        transform.Translate(new Vector3(move, 0, 0));
        movedDistance += move;

        if(Mathf.Abs(movedDistance) >= distance)
        {
            direction *= -1;
            movedDistance = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
