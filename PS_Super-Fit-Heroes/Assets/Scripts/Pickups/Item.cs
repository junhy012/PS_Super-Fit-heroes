using System;
using UnityEngine;
public enum Items
{
    Fries,
    Burger,
    Pizza,
    Chocolate,
    IceCream,
    
    Chicken,
    Steak,
    Broccoli,
    Apple,
    Banana,
}

public class Item : MonoBehaviour
{
    public Items items;
    public int value = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (items)
        {
            case Items.Fries:
                Debug.Log("Agility " + value);
                break;
            case Items.Burger:
                Debug.Log("Strength " + value);
                break;
            case Items.Pizza:
                Debug.Log("Health " + value);
                break;
            case Items.Chocolate:
                Debug.Log("Stamina " + value);
                break;
            case Items.IceCream:
                Debug.Log("All " + -value);
                break;
            case Items.Chicken:
                Debug.Log("Agility " + value);
                break;
            case Items.Steak:
                Debug.Log("Strength " + value);
                break;
            case Items.Broccoli:
                Debug.Log("Health " + value);
                break;
            case Items.Apple:
                Debug.Log("Stamina " + value);
                break;
            case Items.Banana:
                Debug.Log("All " + value);
                break;
            
        }
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
