using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    const float AnimationDamping = 0.1f;
    float speed;
    bool isTired;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, stateMachine.CrossFadeDuration);
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.HealingEvent += stateMachine.HandleHealing;
        stateMachine.InputReader.SkillEvent += stateMachine.OnCastSkill;
        stateMachine.Stamina.OnTired += SetLowStaminaSpeed;
        stateMachine.Stamina.OnEnergetic += SetHighStaminaSpeed;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttack)
        {
            
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0, stateMachine.Attacks));
            return;
        }

        if (stateMachine.InputReader.IsSecondaryAttack)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0, stateMachine.AttacksSecondary));
            return;
        }

        Vector3 direction = CalculateDirection();
        float targetSpeed = stateMachine.InputReader.IsSprint ?
        speed * stateMachine.MultiplyCoefficientSpeed :
        speed;
        Move(direction * targetSpeed, deltaTime);

        if (isTired)
        {
            SetAnimation(0, 0.5f, 1.5f, deltaTime);
        }
        else
        {
            SetAnimation(0, 1, 2, deltaTime);
        }

        RotationByFaceDirection(direction, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.HealingEvent -= stateMachine.HandleHealing;
        stateMachine.InputReader.SkillEvent -= stateMachine.OnCastSkill;
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


    void OnTarget()
    {
        if (!stateMachine.Targeter.SelectedTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetState(stateMachine));
    }

    void OnDodge()
    {
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.Movement));
    }

    void SetAnimation(float stand, float walk, float run, float deltaTime)
    {
        if (stateMachine.InputReader.Movement == Vector2.zero)
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
            stateMachine.Animator.SetFloat(MovementSpeedHash, stand, AnimationDamping, deltaTime);
            return;
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
