using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("LayerMasks and Effects")]
    public LayerMask groundLayer;

    [Header("Speed Controls")]
    public Transform groundCheck;
    public float moveSpeed;
    public float jumpSpeed;
    public float wallSlidingSpeed;

    [Header("Wall Jump Controls")]
    public Transform wallCheck;
    private bool isSliding;
    private bool isWallTouching;
    private bool isGrounded;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    private bool wallJumping;

    Animator playerAnim;
    Rigidbody2D playerRb;

    float playerInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.8f, 0.2f), 0, groundLayer);
        isWallTouching = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f, 0.8f), 0, groundLayer);
        Run();
        Flip();
        Jump();

        if(isWallTouching && !isGrounded && playerInput != 0)
        {
            playerAnim.SetBool("isSliding", true);
            isSliding = true;
        }
        else
        {
            playerAnim.SetBool("isSliding", false);
            isSliding = false;
        }
        if(isSliding && Input.GetKeyDown(KeyCode.Space))
        {
            wallJumping = true;
            Invoke(nameof(StopWallJump), wallJumpDuration);
        }
        Slide();
        WallJump();
    }

    void Slide()
    {
        if (!isSliding)
        {
            return;
        }
        
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, Mathf.Clamp(playerRb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
    }

    void Jump()
    {
        if (!isGrounded)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("jump");
            playerRb.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        if(isSliding)
        {
            return;
        }

        if (playerInput != 0)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
        playerRb.linearVelocity = new Vector2(playerInput * moveSpeed, playerRb.linearVelocity.y);
    }


    void Flip()
    {
        if (playerRb.linearVelocity.x < 0 && isFacingRight == true)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (playerRb.linearVelocity.x > 0 && isFacingRight == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingRight = true;
        }

    }

    void WallJump()
    {
        if (!wallJumping)
        {
            return;
        }
        playerAnim.SetTrigger("jump");
        playerRb.linearVelocity = new Vector2(-playerInput * wallJumpForce.x, wallJumpForce.y);  
    }

    void StopWallJump()
    {
        wallJumping = false;
    }
}
