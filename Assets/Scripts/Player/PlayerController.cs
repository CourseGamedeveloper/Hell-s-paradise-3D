using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Input action for player movement.")]
    private InputAction move = new InputAction(type: InputActionType.Value, expectedControlType: nameof(Vector2));

    [SerializeField]
    [Tooltip("Speed of the player movement.")]
    private float speed = 5f;

    [SerializeField]
    [Tooltip("Jump force for the player.")]
    private float jumpForce = 10f;

    private CharacterController characterController;
    private Vector3 velocity;
 

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on the player!");
        }
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
       
    }

    private void HandleMovement()
    {
        Vector2 moveInput = move.ReadValue<Vector2>();
        float currentSpeed = speed;

        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocity.y = jumpForce;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    
}
