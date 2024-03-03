using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
   

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canDoubleJump;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 1;
    private bool jump;
    private bool doubleJump;
    [SerializeField] private float accelerationTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        canDoubleJump = true;
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        float FeatherCount = GlobalFeather.fAmount; // Replace YourTypeHere with the actual type


        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 targetVelocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, accelerationTime);


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
            isGrounded = false;
            canDoubleJump = true;
        }
        else if (!isGrounded && canDoubleJump && FeatherCount >= 1 && Input.GetButtonDown("Jump"))
        {
            doubleJump = true;
            canDoubleJump = false;
            Debug.Log("FeatherCount after double jump: " + FeatherCount); // Print the FeatherCount to the console

        }
        if (rb.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void FixedUpdate()
    {
       if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
        else if (doubleJump)
        {
            // If the player is moving downwards, add the absolute value of the downwards velocity to the jump force
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * (jumpForce + Mathf.Abs(rb.velocity.y)), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            doubleJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
