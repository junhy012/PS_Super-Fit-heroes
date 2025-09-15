using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enemy.target = other.transform;
            if (enemy.enemyState != ENEMY_STATE.DAMAGED)
                enemy.enemyState = ENEMY_STATE.CHASE;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enemy.target = null;
            enemy.enemyState = ENEMY_STATE.IDLE;
        }
    }
}