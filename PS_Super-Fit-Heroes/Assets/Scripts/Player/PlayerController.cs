using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum PLAYER_STATE
{
    IDLE,
    WALK,
    RUN,
    JUMP,
    ATTACK,
    DAMAGED,
    DEATH,
}

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpHeight = 20f;
    private float attackPower = 1f;

    private int _maxHp = 3; // hp
    public float _currentHp;
    private int _maxStamina = 3;
    private float _currentStamina;

    public PLAYER_STATE playerState = PLAYER_STATE.IDLE;

    public int maxHp
    {
        get { return _maxHp; }
    }

    public float currentHp
    {
        get { return _currentHp; }
    }

    public int maxStamina
    {
        get { return _maxStamina; }
    }

    public float currentStamina
    {
        get { return _currentStamina; }
    }


    public int strength = 3; // power 
    public int agility = 3; // affect moveSpeed and jumpHeight
    public int stamina = 3; // stamina -> Dash or sprint
    public int health = 3;

    private int[] nextLevels = { 3, 6, 10, 15 };
    [SerializeField] private int[] currentLevels = new int[4]; //order -> strength, agility, stamina, health

    private bool isGround;

    private bool isUsingStamina = false;
    private bool isTired = false;

    private float direction;

    public HitBox hitBox;
    CameraShake cameraShake;

    private Rigidbody2D rigidBody2D;
    Animator animator;

    public GameObject HitEffect;

    void Start()
    {
        currentLevels[0] = 1; // strength
        currentLevels[1] = 1; // agility
        currentLevels[2] = 1; // stamina
        currentLevels[3] = 1; // health

        _currentHp = _maxHp;
        _currentStamina = stamina;

        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
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


        FSM(playerState);
    }

    void FSM(PLAYER_STATE state)
    {
        switch (state)
        {
            case PLAYER_STATE.IDLE:
                animator.SetBool("isWalk", false);
                break;
            case PLAYER_STATE.WALK:
                Move(moveSpeed, direction);
                break;
            case PLAYER_STATE.RUN:
                Sprint(direction);
                break;
            case PLAYER_STATE.JUMP:
                Jump();
                break;
            case PLAYER_STATE.ATTACK:
                Attack();
                break;
            // case PLAYER_STATE.DAMAGED:
            //     break;
            case PLAYER_STATE.DEATH:
                break;
        }
    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        direction = horizontal;

        if (playerState == PLAYER_STATE.DAMAGED)
            return;

        if (horizontal != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && _currentStamina > 0 && !isTired) // sprint
            {
                playerState = PLAYER_STATE.RUN;
            }
            else
            {
                playerState = PLAYER_STATE.WALK;
                isUsingStamina = false;
            }
        }
        else
            playerState = PLAYER_STATE.IDLE;


        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                playerState = PLAYER_STATE.JUMP;
        }

        if (Input.GetKeyDown(KeyCode.A) && canAttack)
            playerState = PLAYER_STATE.ATTACK;
    }

    private void Move(float speed, float dir)
    {
        if (dir == 0)
            return;

        animator.SetBool("isWalk", true);
        transform.localScale = new Vector3(dir * 1.8f, 1.8f, 1.8f);
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

        Move(moveSpeed * 1.5f, dir);
    }
    
    public void ResetStamina()
    {
        _currentStamina += Time.deltaTime * 1.5f;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

        if (_currentStamina >= _maxStamina)
            isTired = false;
    }

    public void TakeDamage(Transform enemyFrom)
    {
        if (playerState != PLAYER_STATE.DAMAGED)
        {
            _currentHp -= 1;
            playerState = PLAYER_STATE.DAMAGED;
            animator.SetTrigger("Damaged");
            float dir = transform.position.x < enemyFrom.position.x ? 1f : -1f;
            rigidBody2D.AddForce(Vector2.left * dir * 5f, ForceMode2D.Impulse);
            rigidBody2D.AddForce(Vector2.up * dir * 5f, ForceMode2D.Impulse);
        }
    }


    bool canAttack = true;

    public void Attack()
    {
        canAttack = false;
        animator.SetTrigger("attack");
    }


    public void E_Attack()
    {
        if (hitBox.target != null)
        {
            if (cameraShake != null)
                StartCoroutine(cameraShake.ShakeCamera());

            Instantiate(HitEffect, hitBox.transform.position, Quaternion.identity);
            hitBox.target.TakeDamage(attackPower, currentLevels[0]);
        }
    }

    public void E_AttackEnd()
    {
        canAttack = true;
    }

    public void E_DamagedEnd()
    {
        playerState = PLAYER_STATE.IDLE;
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