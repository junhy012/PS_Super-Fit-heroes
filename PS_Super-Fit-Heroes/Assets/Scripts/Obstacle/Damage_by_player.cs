using UnityEngine;

public class Damage_by_player : MonoBehaviour
{
    [SerializeField] private float maxHp = 3f;
    private float currentHp;

    private void Start()
    {
        currentHp = maxHp;
    }
    public void TakeDamage(float damage, int level = 1)
    {
        currentHp -= damage * level;
        Debug.Log(gameObject.name + " HP: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
