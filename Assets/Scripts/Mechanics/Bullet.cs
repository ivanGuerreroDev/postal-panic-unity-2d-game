using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        public float speed;
        private Vector2 Direction;


        // Start is called before the first frame update
        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            Rigidbody2D.velocity = Direction * speed;
        }

        public void setDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void DestroyBullet()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            EnemyController enemy = collision.collider.GetComponent<EnemyController>();
            if (player != null)
            {
                player.Hit();
            }
            if (enemy != null)
            {
                enemy.Hit();
            }
            DestroyBullet();
        }
    }
}