using System;
using UnityEngine;


public class Attackable : MonoBehaviour
{
    public float hp;

    public Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float damage, int level)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);

        if (enemy != null)
            enemy.enemyState = ENEMY_STATE.DAMAGED;
    }
}