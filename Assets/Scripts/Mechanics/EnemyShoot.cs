using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootController;
    public float lineDistance;
    public LayerMask layerPlayerMask;
    public bool playerInRange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.Raycast(shootController.position, -transform.right, lineDistance, layerPlayerMask);

        if(playerInRange){
                
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootController.position, shootController.position + -transform.right * lineDistance);
    }
}
