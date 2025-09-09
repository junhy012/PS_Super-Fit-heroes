using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpHeight = 10f;

    private Rigidbody2D rigidBody2D;
    private bool isGround;

    public int Hp = 3; // hp
    public int strength = 3; // power 
    public int agility = 3; // affect moveSpeed and jumpHeight
    public int stamina = 3; // stamina -> Dash or sprint

    private int[] nextLevel = new int[] { 3, 6, 10, 15 };
    private int[] currentLevels = new[] { 0, 0, 0, 0 }; //order -> hp, strength, agility, stamina
    
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground")))
            isGround = true;
        else
            isGround = false;

        InputManagement();
    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
            Move(horizontal);

        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (Input.GetKeyDown(KeyCode.LeftShift)) // Sprint
                Sprint();
            if (Input.GetKeyDown(KeyCode.Z)) // Dash
                Dash(horizontal);
        }
    }

    private void Move(float value)
    {
        transform.Translate(value * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        isGround = false;
        rigidBody2D.AddForce(transform.up * (jumpHeight / 2), ForceMode2D.Impulse);
    }

    private void Sprint()
    {
    }

    private void Dash(float dir)
    {
    }

    public void ChangeHp(int value)
    {
        Hp += value;
        Hp = Mathf.Clamp(Hp, 1, 15);
        
        if (Hp >= nextLevel[currentLevels[0]])
        {
            
        }
    }
    
    public void ChangeStrength(int value)
    {
        strength += value;
        strength = Mathf.Clamp(strength, 1, 15);
        
        if (strength >= nextLevel[currentLevels[1]])
        {
            
        }
    }
    
    public void ChangeAgility(int value)
    {
        agility += value;
        agility = Mathf.Clamp(agility, 1, 15);

        if (agility >= nextLevel[currentLevels[2]])
        {
            
        }
    }
    
    public void ChangeStamina(int value)
    {
        stamina += value;
        stamina = Mathf.Clamp(stamina, 1, 15);
        
        if (stamina >= nextLevel[currentLevels[3]])
        {
            
        }
    }

    
}