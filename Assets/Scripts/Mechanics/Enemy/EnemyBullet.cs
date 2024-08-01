using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 5f;
    public int damage = 1;

    private PlayerController player;

    private bool isRight;
    private float bulletTimer = 0.8f;


    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    // Update is called once per frame
    private void Start()
    {
        isRight = player.transform.position.x > transform.position.x;
        Destroy(gameObject, bulletTimer);        
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * (isRight ? Vector2.right : Vector2.left));
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.TryGetComponent(out PlayerController player))
        {
            player.Hit(damage);
            Destroy(gameObject);
        }
    }

}