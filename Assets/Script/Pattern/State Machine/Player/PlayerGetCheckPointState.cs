using UnityEngine;

public class PlayerGetCheckPointState : PlayerBaseState
{
    readonly int GetCheckPointHash = Animator.StringToHash("GetCheckPoint");
    public PlayerGetCheckPointState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.DisableMoment();
        stateMachine.Animator.CrossFadeInFixedTime(GetCheckPointHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        UIManagers.UIManager.ActionCountinue += stateMachine.OnReturnFreeLook;
        UIManagers.UIManager.Rest += stateMachine.Rest;
    }

    public override void Exit()
    {
        stateMachine.DisableMoment();
    }
}
