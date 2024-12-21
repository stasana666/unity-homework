using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigitBody;
    public float jumpHeight = 2.0f;
    private bool isGrounded;
    public float speed = 3.0f;

    public LayerMask groundLayer;
    public float groundCheckDistance = 0.3f;

    public InputAction MoveAction;
    public InputAction JumpAction;

    void Start()
    {
        rigitBody = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        JumpAction.Enable();
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        float move = MoveAction.ReadValue<float>();
        Vector2 movement = new Vector2(MoveAction.ReadValue<float>(), 0f);
        Vector2 position = transform.position;
        position.x += speed * Time.deltaTime * move;
        transform.position = position;

        if (JumpAction.ReadValue<float>() > 0 && IsGrounded())
        {
            rigitBody.velocity = new Vector2(rigitBody.velocity.x,
                Mathf.Sqrt(jumpHeight * -2f * Physics2D.gravity.y));
        }
    }

    private bool IsGrounded()
    {
        print("cast ray");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
