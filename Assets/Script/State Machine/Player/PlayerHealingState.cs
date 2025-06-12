using UnityEngine;

public class PlayerHealingState : PlayerBaseState
{
    readonly int HealingAnimationHash = Animator.StringToHash("Drinking");
    public PlayerHealingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(HealingAnimationHash, stateMachine.CrossFadeDuration);
        Healing();
    }

    public override void Tick(float deltaTime)
    {

        if (GetNormalizedTime(stateMachine.Animator, "Healing") > 0.8f && GetNormalizedTime(stateMachine.Animator, "Healing") < 1f)
        {
            stateMachine.SwitchState(new PlayerTargetState(stateMachine));
        }
    }
    public override void Exit()
    {
    }

    void Healing()
    {
        if (stateMachine.HealingPotion.currentPotion <= 0) { return; }
        stateMachine.Health.RecoverHealth(stateMachine.healing);
        stateMachine.HealingPotion.ReducePotion(stateMachine.reducePotion);
    }

}
