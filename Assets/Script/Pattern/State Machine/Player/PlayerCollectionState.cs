using UnityEngine;

public class PlayerCollectionState : PlayerBaseState
{
    readonly int PickUpAnimatioHash = Animator.StringToHash("Pick_Up");
    string PickUpTag = "Pick_Up";
    public PlayerCollectionState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PickUpAnimatioHash, stateMachine.CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, PickUpTag);
        if (normalizedTime > 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
    }
}
