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
        stateMachine.Animator.Play(TargetingBlendTreeHash);
        stateMachine.InputReader.CancelTargetEvent += OnCancel;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        UpdateAnimation(deltaTime);


        FaceTarget();
        Move(CalculateDirection() * stateMachine.TargetMoveSpeed, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelTargetEvent -= OnCancel;
    }


    void OnCancel()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
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
            stateMachine.Animator.SetFloat(TargetingRightHash, value, AnimationDamping, deltatime);

        }

        if (direction.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, AnimationDamping, deltatime);

        }
        else
        {
            float value = direction.y > 0 ? 1 : -1;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, AnimationDamping, deltatime);
        }
    }

    Vector3 CalculateDirection()
    {
        Vector3 targetMovement = new Vector3();
        targetMovement += stateMachine.transform.right * stateMachine.InputReader.Movement.x;
        targetMovement += stateMachine.transform.forward * stateMachine.InputReader.Movement.y;
        return targetMovement;
    }
}
