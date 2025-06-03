using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    readonly int TargetingForwardHash = Animator.StringToHash("Targeting_Forward");
    readonly int TargetingRightHash = Animator.StringToHash("Targeting_Right");
    const float AnimationDamping = 0.1f;
    Vector2 dodgingDirection;
    float dodgeRemainingTime;
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


        FaceTarget();
        Move(CalculateDirection(deltaTime), deltaTime, false);
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
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeTimeCooldown) { return; }
        stateMachine.SetDodgeTime(Time.time);
        dodgingDirection = stateMachine.InputReader.Movement;
        dodgeRemainingTime = stateMachine.DodgeDuration;
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

    Vector3 CalculateDirection(float deltatime)
    {
        Vector3 targetMovement = new Vector3();

        if (dodgeRemainingTime > 0f)
        {
            targetMovement += stateMachine.transform.right * dodgingDirection.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            targetMovement += stateMachine.transform.forward * dodgingDirection.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            dodgeRemainingTime = Mathf.Max(dodgeRemainingTime - deltatime, 0);
        }
        else
        {

            targetMovement += stateMachine.transform.right * stateMachine.InputReader.Movement.x;
            targetMovement += stateMachine.transform.forward * stateMachine.InputReader.Movement.y;
        }
        return targetMovement;
    }
}
