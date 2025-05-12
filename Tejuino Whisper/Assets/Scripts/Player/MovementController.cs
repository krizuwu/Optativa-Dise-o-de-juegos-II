using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float AnimSpeed = 0.8f; // Animation speed
    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize the movement vector to ensure consistent speed in all directions
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Update the animator parameters
        if (animator != null && movement != Vector2.zero)
        {
            animator.SetFloat("H", movement.x);
            animator.SetFloat("V", movement.y);
            animator.SetFloat("Speed", AnimSpeed);

            animator.speed = AnimSpeed;
        }

        // Check if the player is moving
        bool isMoving = movement != Vector2.zero;

        // Set the isMoving parameter in the animator
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        // Move the player

        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
    }
}
