using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
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

    Vector3 CalculateDirection()
    {
        Vector3 targetMovement = new Vector3();
        targetMovement += stateMachine.transform.right * stateMachine.InputReader.Movement.x;
        targetMovement += stateMachine.transform.forward * stateMachine.InputReader.Movement.y;

        return targetMovement;
    }
}
