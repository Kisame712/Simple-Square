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

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerFeelCollider = GetComponent<CapsuleCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Run();
    }

    void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    void Run()
    {
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
}
