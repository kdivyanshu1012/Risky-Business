using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Animator playerAnimator;

    [Header("Movement")]
    [SerializeField] private float jumpForce = 10f;
    public bool canMove = true;

    private bool isGrounded;
    private float XInput;
    private bool jumpRequested = false;
    private float XVelocity;
    private float YVelocity;

    [Header("Animator Directions")]
    private bool facingRight = true;


    private void Update()
    {
        HandleInput();
        HandleFlip();
        HandleAnimations();
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }


    private void HandleInput()
    {
        // Keyboard movement
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        // Mobile movement
        float mobileInput = MobileInputManager.Horizontal;

        if (mobileInput != 0)
            XInput = mobileInput;
        else
            XInput = keyboardInput;


        // Keyboard jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }


        // Mobile jump
        if (MobileInputManager.JumpPressed && isGrounded)
        {
            jumpRequested = true;
        }
    }


    private void HandleMovement()
    {
        if (!canMove)
            return;

        XVelocity = XInput * PlayerStats.Instance.MovementSpeed;
        YVelocity = playerRigidbody.linearVelocity.y;


        if (jumpRequested)
        {
            AudioManager.Instance.PlaySFX(3);
            YVelocity = jumpForce;
            jumpRequested = false;
        }


        playerRigidbody.linearVelocity =
            new Vector2(XVelocity, YVelocity);
    }


    private void HandleAnimations()
    {
        playerAnimator.SetFloat(
            "horizontalMovement",
            Mathf.Abs(XInput)
        );

        playerAnimator.SetFloat(
            "verticalMovement",
            Mathf.Abs(YVelocity)
        );

        playerAnimator.SetBool(
            "isGrounded",
            isGrounded
        );
    }


    private void HandleFlip()
    {
        if ((playerRigidbody.linearVelocity.x < 0 && facingRight) ||
            (playerRigidbody.linearVelocity.x > 0 && !facingRight))
        {
            Flip();
        }
    }


    private void Flip()
    {
        playerAnimator.transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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