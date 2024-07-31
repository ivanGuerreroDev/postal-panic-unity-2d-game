using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;
        public int health = 1;
        public Bounds Bounds => _collider.bounds;

        // Shoots
        public GameObject Bullet;
        private float LastShoot;
        private bool canMove = false;
        public float lineDistance = 5;
        public LayerMask layerPlayerMask;
        public bool playerInRange;
        private PlayerController player;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            canMove = true;
            player = FindObjectOfType<PlayerController>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {

            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        private void Shoot()
        {
            // shoot to direction of the player.
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            GameObject bullet = Instantiate(Bullet, transform.position + direction, Quaternion.identity);
            bullet.GetComponent<Bullet>().setDirection(direction);
        }

        void Update()
        {
            if (canMove && path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
            playerInRange = Physics2D.Raycast(transform.position, -transform.right, lineDistance, layerPlayerMask);

            if (playerInRange)
            {
                if (Time.time - LastShoot > 1f)
                {
                    Shoot();
                    LastShoot = Time.time;
                }
            }else{
                canMove = true;
            }
        }

        public void Hit()
        {
            health--;
            if (health <= 0) Schedule<EnemyDeath>().enemy = this;
        }
    }
}