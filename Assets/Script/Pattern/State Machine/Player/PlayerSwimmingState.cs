using UnityEngine;

public class PlayerSwimmingState : PlayerBaseState
{
    readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    readonly int FreeLookSwimmingHash = Animator.StringToHash("FreeLookSwimming");
    const float AnimationDamping = 0.1f;
    float speed;
    bool isTired;
    public PlayerSwimmingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookSwimmingHash, stateMachine.CrossFadeDuration);
        stateMachine.Stamina.OnTired += SetLowStaminaSpeed;
        stateMachine.Stamina.OnEnergetic += SetHighStaminaSpeed;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 direction = CalculateDirection();
        float targetSpeed = stateMachine.InputReader.IsSprint ?
        speed * stateMachine.MultiplyCoefficientSpeed :
        speed;

        if (isTired)
        {
            SetAnimation(0.5f, 1.5f, deltaTime);
        }
        else
        {

            SetAnimation(1, 2, deltaTime);
        }


        Move(direction * targetSpeed, deltaTime);
        RotationByFaceDirection(direction, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Stamina.OnTired -= SetLowStaminaSpeed;
        stateMachine.Stamina.OnEnergetic -= SetHighStaminaSpeed;
    }

    void RotationByFaceDirection(Vector3 direction, float deltaTime)
    {
        if (direction != Vector3.zero)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(direction), stateMachine.RotationDamping * deltaTime);
        }
    }
    void SetAnimation(float walk, float run, float deltaTime)
    {
        if (stateMachine.InputReader.Movement == Vector2.zero)
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
            stateMachine.Animator.SetFloat(MovementSpeedHash, 0, AnimationDamping, deltaTime);
            float current = stateMachine.Animator.GetFloat(MovementSpeedHash);
            if (Mathf.Abs(current) < 0.001f)
            {
                stateMachine.Animator.SetFloat(MovementSpeedHash, 0);
            }
        }
        else if (stateMachine.InputReader.IsSprint)
        {
            stateMachine.Stamina.ReduceStamina(stateMachine.sprintStaminaReduce);
            stateMachine.Animator.SetFloat(MovementSpeedHash, run, AnimationDamping, deltaTime);
        }
        else
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
            stateMachine.Animator.SetFloat(MovementSpeedHash, walk, AnimationDamping, deltaTime);
        }
    }

    void SetLowStaminaSpeed()
    {
        speed = stateMachine.LowStaminaSpeed;
        isTired = true;
    }
    void SetHighStaminaSpeed()
    {
        speed = stateMachine.FreeLookMoveSpeed;
        isTired = false;
    }
}
