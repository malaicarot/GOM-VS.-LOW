using UnityEngine;

public class PlayerCollectionState : PlayerBaseState
{
    readonly int PickUpAnimatioHash = Animator.StringToHash("PickUp");
    string PickUpTag = "PickUp";
    Item item;
    public PlayerCollectionState(PlayerStateMachine stateMachine, Item _item) : base(stateMachine)
    {
        item = _item;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PickUpAnimatioHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, PickUpTag);
        if (normalizedTime > 0.9f)
        {
            PlayerInventory.Instance.inventoryObject.AddItem(item.item, 1);
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        item.ReturnToPool();
        UIManagers.Instance.UpdateBrewContent(item.item, false);
    }
}
