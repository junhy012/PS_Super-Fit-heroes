using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(transform);
        }
    }
}