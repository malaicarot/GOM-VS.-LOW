using UnityEngine;

public class PlayerGetCheckPointState : PlayerBaseState
{
    readonly int GetCheckPointHash = Animator.StringToHash("GetCheckPoint");
    string GetCheckPointString = "GetCheckPoint";
    public PlayerGetCheckPointState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(GetCheckPointHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, GetCheckPointString);
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
