using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageAmount = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle!");

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(transform);
                player.TakeDamage(damageAmount);
            }
        }
    }
}
