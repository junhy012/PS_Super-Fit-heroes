using System;
using System.Collections;
using System.Transactions;
using UnityEngine;
public enum Items
{
    Bacon,
    Chicken,
    Brownie,
    Waffle,
    
    Egg,
    Steak,
    Apple,
    Banana,
    
    Goldcoin,
}

public class Item : MonoBehaviour
{
    public Items items;
    public int value;

    public GameObject goldCoinPopup;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            
            switch (items)
            {
                case Items.Bacon:
                    pc.ChangeAgility(value);
                    break;
                case Items.Chicken:
                    pc.ChangeStrength(value);
                    break;
                case Items.Brownie:
                    pc.ChangeStamina(value);
                    break;
                case Items.Waffle:
                    pc.ChangeHealth(value);
                    break;
                
                case Items.Egg:
                    pc.ChangeAgility(value);
                    break;
                case Items.Steak:
                    pc.ChangeStrength(value);
                    break;
                case Items.Apple:
                    pc.ChangeStamina(value);
                    break;
                case Items.Banana:
                    pc.ChangeHealth(value);
                    break;
                
                case Items.Goldcoin:
                    if (goldCoinPopup != null)
                    {
                        goldCoinPopup.SetActive(true);
                    }
                    break;
                        
            }
            Destroy(gameObject);
        }
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // goldCoinPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
