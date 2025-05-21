using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform cameraFollow;
    StateMachine currentState;
    CharacterController controller;
    public Animator animator;
    InputManagers input;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 7f;
    [SerializeField] float sprintSpeed = 12;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float speedTransition = 0.5f;

    [Header("Constant")]
    const float GRAVITY = 9.81f;
    const float THRESHOLD = 0.1f;
    const float CONSTANT_JUMP = 2;
    const string MOVE_ANIMATION_BLEND_NAME = "Speed";
    const string JUMP_ANIMATION_BLEND_NAME = "Jump";

    public string jump_animation_blend_name { get => JUMP_ANIMATION_BLEND_NAME; }

    public string move_animation_blend_name { get => MOVE_ANIMATION_BLEND_NAME; }

    [Header("Local Variables")]
    public float speed;
    float targetSpeed;
    float verticalVerlocity;

    void Awake()
    {
        SetupReferences();
    }
    void Start()
    {
        currentState = new IdleState(this);
        SetState(currentState);
    }

    void Update()
    {
        currentState.Update();
        ProcessRotation();
        CalculateVelocity();
        // CountdownTime.SingletonCountdown.Countdown(4f);
    }

    public void SetState(StateMachine state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = state;
        currentState.Enter();
    }

    void SetupReferences()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        input = GetComponent<InputManagers>();
    }

    public float TarGetSpeed()
    {
        // Process Speed
        if (input.move == Vector2.zero)
        {
            targetSpeed = 0;

        }
        else
        {
            targetSpeed = input.sprint ? sprintSpeed : walkSpeed;
        }
        speed = Mathf.Lerp(speed, targetSpeed, speedTransition * Time.deltaTime);
        return speed;
    }

    public void ProcessMove(float _speed)
    {
        // Process move direction and movement
        Vector3 moveDirection = new Vector3(input.move.x, 0f, input.move.y);
        moveDirection = cameraFollow.TransformDirection(moveDirection);
        moveDirection.y = verticalVerlocity * CONSTANT_JUMP * Time.deltaTime;
        moveDirection *= _speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void ProcessJump()
    {
        verticalVerlocity = Mathf.Sqrt(GRAVITY * jumpHeight * CONSTANT_JUMP);
    }

    void CalculateVelocity()
    {
        if (controller.isGrounded)
        {
            verticalVerlocity = 0;
        }
        else
        {
            verticalVerlocity -= GRAVITY * CONSTANT_JUMP * Time.deltaTime;
        }
        controller.Move(new Vector3(0f, verticalVerlocity, 0f) * Time.deltaTime);
    }

    void ProcessRotation()
    {
        if (input.look.magnitude > THRESHOLD)
        {
            if (CheckMoveInput()) // Chỉ xoay nhân vật theo hướng cam khi nhân vật di chuyển
            {
                Vector3 rotationDirection = cameraFollow.forward; // Hướng xoay là hướng cam
                rotationDirection.y = 0; // Ngăn nhân vật nghiêng theo trục Y 
                Quaternion targetDirection = Quaternion.LookRotation(rotationDirection); //Nhìn về hướng cam
                transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public bool CheckSprintInput()
    {
        if (input.sprint) return true;
        return false;
    }
    public bool CheckMoveInput()
    {
        if (input.move != Vector2.zero) return true;
        return false;
    }
    public bool CheckJumpInput()
    {
        if (input.jump && controller.isGrounded)
        {
            return true;
        }
        return false;
    }
}
