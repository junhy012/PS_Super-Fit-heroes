using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public float health;
    public int maxHealth;
    
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerController playerHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.currentHp;
        maxHealth = playerHealth.maxHp;
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
}
