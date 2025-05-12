using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float AnimSpeed = 0.8f;
    private Vector2 lastDirection = Vector2.down;
    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float shootCooldown = 0.5f;
    private float lastShootTime;
    public float bulletOffset = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player object.");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
  
            movement.Normalize();

            lastDirection = movement;
        }

        Debug.DrawRay(transform.position, lastDirection * 2f, Color.red);

        if (animator != null)
        {
            animator.SetFloat("H", lastDirection.x);
            animator.SetFloat("V", lastDirection.y);
            animator.speed = AnimSpeed;
        }

        bool isMoving = movement != Vector2.zero;


        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Shoot()
    {

        Vector2 spawnPosition = (Vector2)transform.position + lastDirection * bulletOffset;


        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);


        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController == null)
        {
            bulletController = bullet.AddComponent<BulletController>();
        }

       
        bulletController.direction = lastDirection;
        bulletController.speed = bulletSpeed;


        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

       
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);

        Debug.Log("Disparando en dirección: " + lastDirection);
    }
}
