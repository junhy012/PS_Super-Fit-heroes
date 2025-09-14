using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle!");

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(transform);

                player._currentHp -= damageAmount;

                if (player._currentHp <= 0)
                {
                    player.playerState = PLAYER_STATE.DEATH;
                    Debug.Log("Player is Dead!");
                }
            }
        }
    }
}
