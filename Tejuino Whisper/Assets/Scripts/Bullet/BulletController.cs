using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 10f;
    public float lifetime = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime);

  
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0f;
        rb.isKinematic = true; 


        rb.linearVelocity = direction * speed;

        
        Debug.Log("Bala iniciada con direcci�n: " + direction + " y velocidad: " + rb.linearVelocity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;

        Destroy(gameObject);
    }
}
