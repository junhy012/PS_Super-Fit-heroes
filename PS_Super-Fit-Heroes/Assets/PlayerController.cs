using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpHeight = 10f;
    private float attackPower = 1f;

    private int _maxHp = 3; // hp
    private float _currentHp;
    private int _maxStamina = 3;
    private float _currentStamina;

    public int maxHp { get { return _maxHp; } }
    public float currentHp { get { return _currentHp; } }
    public int maxStamina { get { return _maxStamina; } }
    public float currentStamina { get { return _currentStamina; } }
    

    public int strength = 3; // power 
    public int agility = 3; // affect moveSpeed and jumpHeight
    public int stamina = 3; // stamina -> Dash or sprint
    public int health = 3;

    private int[] nextLevels = { 3, 6, 10, 15 };
    private int[] currentLevels = new int[4]; //order -> strength, agility, stamina, health

    private Rigidbody2D rigidBody2D;
    private bool isGround;

    private bool isUsingStamina = false;
    private bool isTired = false;

    void Start()
    {
        currentLevels[0] = 1; // strength
        currentLevels[1] = 1; // agility
        currentLevels[2] = 1; // stamina
        currentLevels[3] = 1; // health

        _currentHp = _maxHp;
        _currentStamina = stamina;

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
            if (Input.GetKey(KeyCode.LeftShift) && _currentStamina > 0 && horizontal != 0 && !isTired) // Sprint
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
        _currentStamina -= Time.deltaTime * 3f;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

        if (_currentStamina <= 0.1f)
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
        _currentStamina += Time.deltaTime * 1.5f;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

        if (_currentStamina >= _maxStamina)
            isTired = false;
    }
    public void TakeDamage()
    {
        _currentHp -= 1;
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

    public void ChangeHealth(int value)
    {
        health += value;
        health = Mathf.Clamp(health, 1, 15);
        CheckLevel(3, health);
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
                _maxStamina += value;
                break;
            case 3:
                _maxHp += value;
                break;
        }
    }

    #endregion
}