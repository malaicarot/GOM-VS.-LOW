using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    readonly int TargetingForwardHash = Animator.StringToHash("Targeting_Forward");
    readonly int TargetingRightHash = Animator.StringToHash("Targeting_Right");
    const float AnimationDamping = 0.1f;
    public PlayerTargetState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, stateMachine.CrossFadeDuration);
        stateMachine.InputReader.CancelTargetEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttack)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
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
            stateMachine.TargetMoveSpeed * stateMachine.MultiplyCoefficientSpeed :
            stateMachine.TargetMoveSpeed;

        FaceTarget();
        Move(CalculateTargetDirection() * targetSpeed, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelTargetEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;
    }


    void OnCancel()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    void OnDodge()
    {
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.Movement));
    }
    void UpdateAnimation(float deltatime)
    {
        Vector3 direction = stateMachine.InputReader.Movement;

        if (direction.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, AnimationDamping, deltatime);
        }
        else
        {
            float value = direction.x > 0 ? 1 : -1;
            if (IsSprint()) { value *= 2; }
            stateMachine.Animator.SetFloat(TargetingRightHash, value, AnimationDamping, deltatime);
        }

        if (direction.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, AnimationDamping, deltatime);

        }
        else
        {
            float value = direction.y > 0 ? 1 : -1;
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
