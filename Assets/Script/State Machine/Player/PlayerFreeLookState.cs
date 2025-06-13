using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    const float AnimationDamping = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, stateMachine.CrossFadeDuration);
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.HealingEvent += stateMachine.HandleHealing;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttack)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        Vector3 direction = CalculateDirection();
        float targetSpeed = stateMachine.InputReader.IsSprint ?
        stateMachine.FreeLookMoveSpeed * stateMachine.MultiplyCoefficientSpeed :
        stateMachine.FreeLookMoveSpeed;
        Move(direction * targetSpeed, deltaTime);

        if (stateMachine.InputReader.Movement == Vector2.zero)
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
            stateMachine.Animator.SetFloat(MovementSpeedHash, 0, AnimationDamping, deltaTime);
            return;
        }
        else if (stateMachine.InputReader.IsSprint)
        {
            stateMachine.Stamina.ReduceStamina(stateMachine.sprintStaminaReduce);
            stateMachine.Animator.SetFloat(MovementSpeedHash, 2, AnimationDamping, deltaTime);
        }
        else
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
            stateMachine.Animator.SetFloat(MovementSpeedHash, 1, AnimationDamping, deltaTime);
        }

        RotationByFaceDirection(direction, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.HealingEvent -= stateMachine.HandleHealing;

    }

    void RotationByFaceDirection(Vector3 direction, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(direction), stateMachine.RotationDamping * deltaTime);
    }


    void OnTarget()
    {
        if (!stateMachine.Targeter.SelectedTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetState(stateMachine));
    }

    void OnDodge()
    {
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.Movement));
    }
}
