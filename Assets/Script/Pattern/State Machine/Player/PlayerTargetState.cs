using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    readonly int TargetingForwardHash = Animator.StringToHash("Targeting_Forward");
    readonly int TargetingRightHash = Animator.StringToHash("Targeting_Right");
    const float AnimationDamping = 0.1f;
    float speed;
    bool isTired;

    public PlayerTargetState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, stateMachine.CrossFadeDuration);
        stateMachine.InputReader.CancelTargetEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += stateMachine.OnDodge;
        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;
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

        if (stateMachine.Targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        UpdateAnimation(deltaTime);

        float targetSpeed = stateMachine.InputReader.IsSprint ?
            speed * stateMachine.MultiplyCoefficientSpeed :
            speed;
        if (targetSpeed >= speed)
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
        }
        else if (targetSpeed >= speed * stateMachine.MultiplyCoefficientSpeed)
        {
            stateMachine.Stamina.ReduceStamina(stateMachine.sprintStaminaReduce);
        }

        FaceTarget();
        Move(CalculateTargetDirection() * targetSpeed, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelTargetEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= stateMachine.OnDodge;
        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;
        stateMachine.InputReader.HealingEvent -= stateMachine.HandleHealing;
        stateMachine.InputReader.SkillEvent -= stateMachine.OnCastSkill;
        stateMachine.Stamina.OnTired -= SetLowStaminaSpeed;
        stateMachine.Stamina.OnEnergetic -= SetHighStaminaSpeed;
    }


    void OnCancel()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    void SetLowStaminaSpeed()
    {
        speed = stateMachine.LowStaminaSpeed;
        isTired = true;
    }
    void SetHighStaminaSpeed()
    {
        speed = stateMachine.TargetMoveSpeed;
        isTired = false;
    }

    void UpdateAnimation(float deltatime)
    {
        Vector3 direction = stateMachine.InputReader.Movement;
        Debug.Log(direction.x);
        Debug.Log(direction.y);
        Debug.Log(direction.z);

        if (direction == Vector3.zero)
        {
            stateMachine.Stamina.RecoveryStamina(stateMachine.staminaRecovery);
        }
        // else
        // {

        //     stateMachine.Stamina.ReduceStamina(stateMachine.sprintStaminaReduce);
        // }

        if (direction.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, AnimationDamping, deltatime);
        }
        else
        {
            float index;
            if (isTired)
            {
                index = 0.5f;
            }
            else
            {
                index = 1;
            }

            float value = direction.x > 0 ? index : -1;
            if (IsSprint()) { value *= 2; }
            stateMachine.Animator.SetFloat(TargetingRightHash, value, AnimationDamping, deltatime);
        }

        if (direction.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, AnimationDamping, deltatime);
        }
        else
        {
            float index;
            if (isTired)
            {
                index = 0.5f;
            }
            else
            {
                index = 1;
            }

            float value = direction.y > 0 ? index : -1;
            if (IsSprint()) { value *= 2; }
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, AnimationDamping, deltatime);
        }
    }

    Vector3 CalculateTargetDirection()
    {
        Vector3 targetMovement = new Vector3();

        targetMovement += stateMachine.transform.right * stateMachine.InputReader.Movement.x;
        targetMovement += stateMachine.transform.forward * stateMachine.InputReader.Movement.y;

        return targetMovement;
    }


}
