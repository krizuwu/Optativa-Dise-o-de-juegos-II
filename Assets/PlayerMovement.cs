using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Obtener la entrada del usuario
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Crear un vector de movimiento
        Vector3 move = new Vector3(moveX, moveY, 0f);

        // Mover el sprite
        transform.position += move * moveSpeed * Time.deltaTime;


        Vector2 direction = new Vector2(moveX, moveY);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                Animator.SetFloat("X", 1);
                Animator.SetFloat("Y", 0);
            }
            else
            {
                Animator.SetFloat("X", -1);
                Animator.SetFloat("Y", 0);

            }
        }
        else
        {
            if (direction.y > 0)
            {
                Animator.SetFloat("Y", 1);
                Animator.SetFloat("X", 0);
            }
            else
            {
                Animator.SetFloat("Y", -1);
                Animator.SetFloat("X", 0);
            }
        }
    }
}
