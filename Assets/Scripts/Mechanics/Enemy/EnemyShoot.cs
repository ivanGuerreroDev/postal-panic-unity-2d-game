using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootController;
    public float lineDistance = 3f;
    public LayerMask playerLayer;
    public bool playerInRange;
    public GameObject BulletPrefab;
    public float shootRate = 2.5f;
    private float lastShootTime = 0f;
    private float waitShootTime = 0.5f;
    public float distance;
    void Start()
    {
    }

    void Update()
    {
        var player = FindObjectOfType<PlayerController>();
        var positionPlayer = player.transform.position;
        distance = Mathf.Abs(positionPlayer.x - transform.position.x);
        playerInRange = distance < lineDistance;
        if (playerInRange)
        {
            if (Time.time > shootRate + lastShootTime)
            {
                lastShootTime = Time.time;
                Invoke(nameof(Shoot), waitShootTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootController.position, shootController.position + transform.right * lineDistance);
    }

    private void Shoot()
    {
        Instantiate(BulletPrefab, transform.position, shootController.rotation);
    }
}