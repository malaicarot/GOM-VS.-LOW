using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    readonly int BlockHash = Animator.StringToHash("Block");
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, stateMachine.CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        stateMachine.Health.SetParry(true);
        if (!stateMachine.InputReader.IsBlocking)
        {
            if (stateMachine.Targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetState(stateMachine));
                return;
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetParry(false);
    }
}
