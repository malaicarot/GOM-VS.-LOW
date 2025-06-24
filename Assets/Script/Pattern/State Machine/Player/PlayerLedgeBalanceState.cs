using UnityEngine;

public class PlayerLedgeBalanceState : PlayerBaseState
{
    readonly int LedgeBalanceAnimationHash = Animator.StringToHash("Ledge_Balance");
    string LedgeBalanceTag = "Ledge_Balance";
    public PlayerLedgeBalanceState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LedgeBalanceAnimationHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, LedgeBalanceTag);
        if (normalizedTime > 1f)
        {
            if (stateMachine.Targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }
    public override void Exit()
    {
    }
}
