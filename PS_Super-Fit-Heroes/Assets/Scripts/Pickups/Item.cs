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
	public static string description;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            
            switch (items)
            {
                case Items.Bacon:
                    pc.ChangeAgility(value);
                    Destroy(gameObject);
                    break;
                case Items.Chicken:
                    pc.ChangeStrength(value);
                    Destroy(gameObject);
                    break;
                case Items.Brownie:
                    pc.ChangeStamina(value);
                    Destroy(gameObject);
                    break;
                case Items.Waffle:
                    pc.ChangeHealth(value);
                    Destroy(gameObject);
                    break;
                
                case Items.Egg:
                    pc.ChangeAgility(value);
                    Destroy(gameObject);
                    break;
                case Items.Steak:
                    pc.ChangeStrength(value);
                    Destroy(gameObject);
                    break;
                case Items.Apple:
                    pc.ChangeStamina(value);
                    Destroy(gameObject);
                    break;
                case Items.Banana:
                    pc.ChangeHealth(value);
                    Destroy(gameObject);
                    break;
                
                case Items.Goldcoin:
                    if (goldCoinPopup != null)
                    {
                        goldCoinPopup.SetActive(true);
                    }
                    break;
            }
        }
   
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && items == Items.Goldcoin)
        {
            if (goldCoinPopup != null)
            {
                goldCoinPopup.SetActive(false);
            }
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
