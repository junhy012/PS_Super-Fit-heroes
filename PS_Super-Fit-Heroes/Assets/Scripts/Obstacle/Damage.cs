using UnityEngine;

public class Damage : MonoBehaviour
{
    private GameObject player;
    private Vector2 respawnPoint;   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        respawnPoint = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touch the obstacle!!!");
            player.transform.position = respawnPoint;
        }
    }
}
