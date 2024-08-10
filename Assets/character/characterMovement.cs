using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;         // Movement speed of the player
    public float jumpForce = 10f;        // Force applied when the player jumps

    [Header("Ground Check")]
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f); // Size of the box for ground check
    public Transform groundCheck; // Reference to the GroundCheck position
    public LayerMask groundLayer;        // Layers that are considered as ground
    public float groundDistance;

    private Rigidbody2D rb;              // Reference to the player's Rigidbody2D component
    private bool isGrounded;             // Flag to check if the player is on the ground
    private float horizontalInput;       // Stores horizontal input for movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Check if the player is grounded using BoxCast
        isGrounded = Physics2D.BoxCast(new Vector2(groundCheck.position.x, groundCheck.position.y - groundDistance) , groundCheckSize, 0f, Vector2.down, 0.1f, groundLayer);

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }

        // Flip the player sprite based on movement direction
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); // Face right
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); // Face left
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply vertical force for jumping
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo to visualize the ground check area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(groundCheck.position.x, groundCheck.position.y - groundDistance), groundCheckSize);
    }
}