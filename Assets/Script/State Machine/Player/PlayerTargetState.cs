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
        Debug.Log(stateMachine.Targeter.currentTarget.name);

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
}
