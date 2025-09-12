using UnityEngine;


public class Attackable : MonoBehaviour
{
    public float hp;

    public void TakeDamage(float damage, int level)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }
}