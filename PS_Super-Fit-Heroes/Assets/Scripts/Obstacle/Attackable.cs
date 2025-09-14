using UnityEngine;

public class Attackable : MonoBehaviour
{
    public float hp = 3f; 
    public int contactDamage = 1; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(transform);
                player._currentHp -= contactDamage;

                if (player._currentHp <= 0)
                {
                    player.playerState = PLAYER_STATE.DEATH;
                    Debug.Log("Player is Dead!");
                }
            }
        }
    }

    public void TakeDamage(float damage, int level)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
