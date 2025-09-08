using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider slider;
    public int health;
    public int maxHealth = 10;

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
