using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    
    private Vector2 moveInput;
    private PlayerInputActions inputActions;
    
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }
    
    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Disable();
    }
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        UpdateAnimationParameters();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        UpdateAnimationParameters();
    }
    
    private void UpdateAnimationParameters()
    {
        if (animator != null)
        {
            bool isMoving = moveInput.sqrMagnitude > 0.01f;
            animator.SetBool("IsMoving", isMoving);
            
            // Reset all directional bools
            animator.SetBool("MovingUp", false);
            animator.SetBool("MovingDown", false);
            animator.SetBool("MovingLeft", false);
            animator.SetBool("MovingRight", false);
            
            // Set the appropriate directional bool based on dominant direction
            if (isMoving)
            {
                if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                {
                    // Horizontal movement is dominant
                    if (moveInput.x > 0)
                        animator.SetBool("MovingRight", true);
                    else
                        animator.SetBool("MovingLeft", true);
                }
                else
                {
                    // Vertical movement is dominant
                    if (moveInput.y > 0)
                        animator.SetBool("MovingUp", true);
                    else
                        animator.SetBool("MovingDown", true);
                }
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (rb != null)
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}