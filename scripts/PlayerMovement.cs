using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim; // Reference to the Animator component
    private SpriteRenderer spriteRenderer; // Reference to flip the character sprite
    private bool isGrounded;

    // Spawn point
    private Transform spawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find object with Spawn tag
        GameObject spawnObj = GameObject.FindGameObjectWithTag("Spawn");

        if (spawnObj != null)
        {
            spawnPoint = spawnObj.transform;
        }
    }

    void Update()
    {
        float moveInput = 0;

        // LEFT
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1;
        }

        // RIGHT
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // JUMP
        if ((Keyboard.current.spaceKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame ||
             Keyboard.current.wKey.wasPressedThisFrame) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Handle Animations and Sprite Flipping
        UpdateAnimations(moveInput);
    }

    private void UpdateAnimations(float moveInput)
    {
        if (anim == null) return;

        // 1. Idle vs. Run: Use the absolute horizontal input/velocity
        // If moveInput is not 0, we are moving.
        bool isMoving = moveInput != 0;
        anim.SetBool("isRunning", isMoving);

        // 2. Grounded status for Jumping/Falling states
        anim.SetBool("isGrounded", isGrounded);

        // 3. Optional: Pass vertical speed if you want separate jump and fall states
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        // 4. Flip the sprite visually based on direction
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Facing Right
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;  // Facing Left
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;

            // Stop movement after respawn
            rb.linearVelocity = Vector2.zero;
        }
    }
}