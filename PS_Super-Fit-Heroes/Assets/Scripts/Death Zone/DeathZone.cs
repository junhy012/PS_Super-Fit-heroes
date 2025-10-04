using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private GameObject player;
    private Vector2 startPosition;   

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
       
        startPosition = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            player.transform.position = startPosition;

    
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            Debug.Log("Player reach the death zone");
        }
    }
}
