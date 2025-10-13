using UnityEngine;

public class PlayerCollectionState : PlayerBaseState
{
    readonly int PickUpAnimatioHash = Animator.StringToHash("PickUp");
    string PickUpTag = "PickUp";
    public PlayerCollectionState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PickUpAnimatioHash, stateMachine.CrossFadeDuration);
        PlayerInventories.Instance.AddItemByType(stateMachine.item.ReturnItem());
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
        UIManagers.Instance.UpdateBrewContent();
    }
}
