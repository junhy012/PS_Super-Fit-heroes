using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageAmount = 1f;
    private Vector2 startPosition;
    private GameObject player;
    private PlayerController pc;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            startPosition = player.transform.position;
            pc = player.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pc != null)
        {
            Debug.Log("Player hit an obstacle!");

            pc.TakeDamage(transform);
            pc.hp -= damageAmount;
            Debug.Log("Player HP: " + pc.hp);

 
            if (pc.hp <= 0)
            {
                Debug.Log("Player is Dead! Respawning...");

                player.transform.position = startPosition;


                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }

                pc.hp = 3f;
            }
        }
    }
}
