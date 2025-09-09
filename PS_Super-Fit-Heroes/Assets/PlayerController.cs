using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpHeight = 10f;
    private float attackPower = 1f;

    private int maxHp = 3; // hp
    private float currentHp;
    private int maxStamina = 3;
    public float currentStamina;

    public int strength = 3; // power 
    public int agility = 3; // affect moveSpeed and jumpHeight
    public int stamina = 3; // stamina -> Dash or sprint

    private int[] nextLevels = { 3, 6, 10, 15 };
    private int[] currentLevels = new int[3]; //order -> strength, agility, stamina

    private Rigidbody2D rigidBody2D;
    private bool isGround;

    private bool isUsingStamina = false;
    private bool isTired = false;

    void Start()
    {
        currentLevels[0] = 1; // strength
        currentLevels[1] = 1; // agility
        currentLevels[2] = 1; // stamina

        currentHp = maxHp;
        currentStamina = stamina;

        rigidBody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground")))
            isGround = true;
        else
            isGround = false;

        InputManagement();

        if (!isUsingStamina)
            ResetStamina();

    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
            Move(moveSpeed, horizontal);

        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && horizontal != 0 && !isTired) // Sprint
                Sprint(horizontal);
            else
                isUsingStamina = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && canAttack)
            Attack();
        if (Input.GetKeyDown(KeyCode.Z)) // Dash
            Dash(horizontal);
    }

    private void Move(float speed, float dir)
    {
        if (dir == 0)
            return;
        transform.localScale = new Vector3(dir, 1, 1);
        transform.Translate(dir * speed * Time.deltaTime, 0, 0);
    }

    private void Jump()
    {
        isGround = false;
        rigidBody2D.AddForce(transform.up * (jumpHeight / 2), ForceMode2D.Impulse);
    }

    private void Sprint(float dir)
    {
        isUsingStamina = true;
        currentStamina -= Time.deltaTime * 3f;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina <= 0.1f)
        {
            isTired = true;
            isUsingStamina = false;
            return;
        }
        
        Move(moveSpeed * 1.2f, dir);
    }

    private void Dash(float dir)
    {
    }

    public void ResetStamina()
    {
        currentStamina += Time.deltaTime * 1.5f;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina >= maxStamina)
            isTired = false;
    }
    public void TakeDamage()
    {
        currentHp -= 1;
    }
    
    bool canAttack = true;

    public void Attack()
    {
        canAttack = false;
        Debug.Log("attack!");
        StartCoroutine(AttackCoolTime());
    }

    IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    
    #region stat

    public void ChangeStrength(int value)
    {
        strength += value;
        strength = Mathf.Clamp(strength, 1, 15);
        CheckLevel(0, strength);
    }

    public void ChangeAgility(int value)
    {
        agility += value;
        agility = Mathf.Clamp(agility, 1, 15);
        CheckLevel(1, agility);
    }

    public void ChangeStamina(int value)
    {
        stamina += value;
        stamina = Mathf.Clamp(stamina, 1, 15);
        CheckLevel(2, stamina);
    }

    private void CheckLevel(int statIndex, int stat)
    {
        int newLevel = 0;

        for (int i = 0; i < nextLevels.Length; i++)
        {
            if (nextLevels[i] <= stat)
            {
                newLevel += 1;
            }
        }

        if (currentLevels[statIndex] < newLevel)
        {
            currentLevels[statIndex] += 1;
            UpdateStat(statIndex, 1);
        }
        else if (currentLevels[statIndex] > newLevel)
        {
            currentLevels[statIndex] -= 1;
            UpdateStat(statIndex, -1);
        }
    }

    private void UpdateStat(int statIndex, int value)
    {
        switch (statIndex)
        {
            case 0:
                attackPower += value;
                break;
            case 1:
                moveSpeed += value;
                jumpHeight += value;
                break;
            case 2:
                maxStamina += value;
                break;
        }
    }

    #endregion
}