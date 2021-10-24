using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    const string PLAYER = "Player";

    public float distance = 3.0f;
    public float direction = -1.0f;
    public float speed = 2.0f;

    private float movedDistance = 0.0f;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag(PLAYER))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag(PLAYER))
        {
            other.transform.SetParent(null);
        }
    }
}
