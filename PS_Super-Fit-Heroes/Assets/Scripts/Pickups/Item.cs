using System;
using System.Transactions;
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
    public int value;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            
            switch (items)
            {
                case Items.Fries:
                    pc.ChangeAgility(value);
                    break;
                case Items.Burger:
                    pc.ChangeStrength(value);
                    break;
                case Items.Pizza:
                    pc.ChangeStamina(value);
                    break;
                case Items.Chocolate:
                    pc.ChangeAgility(value);
                    pc.ChangeStrength(value);
                    pc.ChangeStamina(value);
                    break;
                case Items.IceCream:
                    // pc.ChangeHealth(value);
                    // pc.TakeDamage();
                    break;
                case Items.Chicken:
                    pc.ChangeAgility(value);
                    break;
                case Items.Steak:
                    pc.ChangeStrength(value);
                    break;
                case Items.Broccoli:
                    pc.ChangeStamina(value);
                    break;
                case Items.Apple:
                    pc.ChangeAgility(value);
                    pc.ChangeStrength(value);
                    pc.ChangeStamina(value);
                    break;
                case Items.Banana:
                    pc.ChangeHealth(value);
                    break;
            
            }
            Destroy(gameObject);
        }
        
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
