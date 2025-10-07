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
    
    public AudioClip pickupItemSound;
    public GameObject pickupEffect;

    public GameObject goldCoinPopup;
    public string description;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            switch (items)
            {
                case Items.Bacon:
                    pc.ChangeAgility(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;
                case Items.Chicken:
                    pc.ChangeStrength(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;
                case Items.Brownie:
                    pc.ChangeStamina(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;
                case Items.Waffle:
                    pc.ChangeHealth(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;

                case Items.Egg:
                    pc.ChangeAgility(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;
                case Items.Steak:
                    pc.ChangeStrength(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;
                case Items.Apple:
                    pc.ChangeStamina(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;
                case Items.Banana:
                    pc.ChangeHealth(value);
                    PlayPickupEffects();
                    Destroy(gameObject);
                    break;

                case Items.Goldcoin:
                    if (goldCoinPopup != null)
                    {
                        PlayPickupEffects();
                        InformationPopup info = goldCoinPopup.GetComponent<InformationPopup>();
                        info.information = description;
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

    private void PlayPickupEffects()
    {
        if (pickupItemSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupItemSound, transform.position);
        }

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
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