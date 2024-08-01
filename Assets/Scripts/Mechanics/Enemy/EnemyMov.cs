using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    public float speed = .8f;
    public bool directionRight = true;
    public float timeToChangeDirection = 4f;
    public float timer = 4f;
    public Transform target;
    public bool shouldFollowPlayer = false;
    public float distance;
    public float distanceToFollow = 3f;
    private Rigidbody2D rb2d;
    void Start()
    {
        timer = timeToChangeDirection;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
    }

    void Update()
    {
        distance = Mathf.Abs(target.position.x - transform.position.x);
        shouldFollowPlayer = distance < distanceToFollow;
        if (shouldFollowPlayer)
        {
            if (distance > 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                var direction = target.position - transform.position;
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-(Mathf.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            if (directionRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.localScale = new Vector3(-(Mathf.Abs(transform.localScale.x)), transform.localScale.y, transform.localScale.z);
            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                directionRight = !directionRight;
                timer = timeToChangeDirection;
            }
        }
    }
}
