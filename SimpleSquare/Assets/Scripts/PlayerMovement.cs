using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Controls")]
    public float moveSpeed;
    public float jumpSpeed;

    Animator playerAnim;
    Rigidbody2D playerRb;
    CapsuleCollider2D playerFeelCollider;

    Vector2 playerInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerFeelCollider = GetComponent<CapsuleCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Run();
        Flip();
    }

    void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    void Run()
    {
        if(playerInput.x != 0)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
        playerRb.linearVelocity = new Vector2(playerInput.x * moveSpeed, playerRb.linearVelocity.y);
    }

    void OnJump()
    {
        if (!playerFeelCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        playerRb.linearVelocity += new Vector2(0f, jumpSpeed);
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
}
