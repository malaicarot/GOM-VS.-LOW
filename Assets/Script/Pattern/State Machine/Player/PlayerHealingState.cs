using UnityEngine;

public class PlayerHealingState : PlayerBaseState
{
    readonly int HealingAnimationHash = Animator.StringToHash("Drinking");
    public PlayerHealingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Healing();
        stateMachine.Animator.CrossFadeInFixedTime(HealingAnimationHash, stateMachine.CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Healing") > 0.8f && GetNormalizedTime(stateMachine.Animator, "Healing") <= 1f)
        {
            ReturnToLocomotion();
        }
    }
    public override void Exit()
    {
    }

    void Healing()
    {
        if (stateMachine.HealingPotion.currentPotion <= 0)
        {
            ReturnToLocomotion();
        }

        stateMachine.Health.RecoverHealth(stateMachine.healing);
        stateMachine.HealingPotion.ReducePotion(stateMachine.reducePotion);
    }
}
