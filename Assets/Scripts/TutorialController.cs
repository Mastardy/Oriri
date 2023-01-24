using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    private Animator animator;
    private CharacterController charController;

    [SerializeField] private InputAction prisonerJump;
    [SerializeField] private InputAction prisonerMovement;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float gravity = 5f;
    [SerializeField] private float jumpSpeed = 50f;
    private float verticalSpeed = 0;
    
    private static readonly int velocityCache = Animator.StringToHash("velocity");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        
        prisonerMovement.Enable();
        prisonerJump.Enable();
    }

    private void Update()
    {
        var movement = prisonerMovement.ReadValue<Vector2>();
        if(movement.magnitude > 1) movement.Normalize();

        Vector3 velocity = Vector3.forward * movement.x + Vector3.left * movement.y;
        velocity *= Time.deltaTime * moveSpeed;

        verticalSpeed -= gravity * Time.deltaTime;
        if (charController.isGrounded)
        {
            verticalSpeed = 0;
            if (prisonerJump.IsPressed()) verticalSpeed = jumpSpeed;
        }
        
        velocity.y = verticalSpeed;
        
        charController.Move(velocity);

        var animatorVelocity = charController.velocity / moveSpeed;
        animatorVelocity.y = 0;
        if (animatorVelocity.magnitude > 1) animatorVelocity.Normalize();
        
        animator.SetFloat(velocityCache, animatorVelocity.magnitude);

        var lookAt = velocity;
        lookAt.y = 0;
        
        transform.LookAt(transform.position + lookAt);
    }
}
