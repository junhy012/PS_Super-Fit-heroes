using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Ground Check Settings")]
    public Transform groundCheck;          // Empty object at player's feet
    public float groundCheckRadius = 0.2f; // Small radius for ground detection
    public LayerMask groundLayer;          // Assign your ground layer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckGround();
    }

    // Horizontal movement
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    // Jumping
    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset vertical velocity
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Check if player is on the ground
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Coin pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))  // Tag must match exactly
        {
            Destroy(collision.gameObject);
        }
    }
}
