using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ENEMY_STATE
{
    IDLE,
    WALK,
    CHASE,
    DAMAGED,
    DEATH,
}

public class Enemy : MonoBehaviour
{
    public ENEMY_STATE enemyState = ENEMY_STATE.IDLE;

    private float moveSpeed = 2f;

    public int moveDir = 0;

    public Animator animator;

    public Transform target;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveFlag());
    }

    private void Update()
    {
        CheckGround();
        FSM();
    }


    private LayerMask objMasks;

    private void CheckGround()
    {
        Vector2 start = new Vector2(transform.position.x + moveDir, transform.position.y);
        RaycastHit2D ground = Physics2D.Raycast(start, Vector2.down, 3, LayerMask.GetMask("Ground"));
        RaycastHit2D obj = Physics2D.Raycast(transform.position, Vector2.right * moveDir, 1,
            LayerMask.GetMask("obj"));


        if (ground.collider == null)
        {
            Debug.DrawRay(start, Vector2.down * 3, Color.red);
        }
        else
        {
            Debug.DrawRay(start, Vector2.down * 3, Color.green);
        }

        if (obj.collider == null)
        {
            Debug.DrawRay(transform.position, Vector2.right * moveDir * 1, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.right * moveDir * 1, Color.green);
        }

        if (ground.collider == null)
        {
            moveDir *= -1;
        }

        if (obj.collider != null)
        {
            moveDir *= -1;
        }
    }

    IEnumerator MoveFlag()
    {
        while (true)
        {
            float rand = Random.Range(1.0f, 3.1f);

            float moveflag = Random.value;

            if (moveflag >= 0.6f)
            {
                enemyState = ENEMY_STATE.IDLE;
                moveDir = 0;
            }
            else
            {
                enemyState = ENEMY_STATE.WALK;
                moveDir = (Random.value > 0.5f) ? 1 : -1;
            }

            yield return new WaitForSeconds(rand);
        }
    }

    void FSM()
    {
        switch (enemyState)
        {
            case ENEMY_STATE.IDLE:
                target = null;
                animator.SetBool("isWalk", false);
                break;
            case ENEMY_STATE.WALK:
                animator.SetBool("isWalk", true);
                transform.localScale = new Vector3(moveDir * 1.8f, 1.8f, 1.8f);
                transform.Translate(moveDir * moveSpeed * Time.deltaTime, 0, 0);
                break;
            case ENEMY_STATE.CHASE:
                if (target != null)
                {
                    animator.SetBool("isWalk", true);

                    if (target.transform.position.x > transform.position.x)
                        moveDir = 1;
                    else
                        moveDir = -1;

                    transform.localScale = new Vector3(moveDir * 1.8f, 1.8f, 1.8f);
                    transform.Translate(moveDir * moveSpeed * Time.deltaTime, 0, 0);
                }

                break;
            case ENEMY_STATE.DAMAGED:
                moveDir = 0;
                StopCoroutine(MoveFlag());
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
                    animator.SetTrigger("isDamaged");
                break;
            case ENEMY_STATE.DEATH:
                break;
        }
    }

    public void E_DamagedEnd()
    {
        StartCoroutine(MoveFlag());
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(transform);
        }

       
    }
}