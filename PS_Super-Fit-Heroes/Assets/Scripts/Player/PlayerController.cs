using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;

    [SerializeField] private float strength;  
    [SerializeField] private float agility; 
    [SerializeField] private float stamina; 
    

    private Rigidbody2D rigidBody2D;

    private bool isGround;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeStrength(-1);
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground")))
            isGround = true;
        else
            isGround = false;
        
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
            Move(horizontal);

        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
    }
    
    private void Move(float value)
    {
        transform.Translate(value * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        isGround = false;
        rigidBody2D.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }


    public void ChangeAgility(float value)
    {
        agility += value;
        
        agility = Mathf.Clamp(agility, 0,10);
        
        moveSpeed = agility;
        jumpHeight = agility * 2f;
    }
    
    public void ChangeStrength(float value)
    {
        strength += value;
        strength = Mathf.Clamp(strength, 0,5);
    }
    
    public void ChangeStamina(float value)
    {
        stamina += value;
        stamina = Mathf.Clamp(stamina, 0,10);
    }
   
}