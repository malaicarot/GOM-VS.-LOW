using UnityEngine;

public class PlayerSpecialAttack : PlayerBaseState
{
    readonly int SpecialAttackHash;
    readonly string SpecialAttackTag;
    SpecialEffectsData effect;
    public PlayerSpecialAttack(PlayerStateMachine stateMachine, SpecialEffectsData _effect) : base(stateMachine)
    {
        effect = _effect;
        SpecialAttackHash = effect.AnimationHash;
        SpecialAttackTag = effect.AnimationTag;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(SpecialAttackHash, stateMachine.CrossFadeDuration);
        foreach (AttackDealDamage attackDamage in stateMachine.AttackDealDamage)
        {
            attackDamage.SetAttack(stateMachine.PlayerStats.CalculateCritical(stateMachine.Attacks[0].AttackDamage * effect.AttackCoefficient), effect.AttackKnockback);
        }
    }

    public override void Tick(float deltaTime)
    {
        float normalized = GetNormalizedTime(stateMachine.Animator, SpecialAttackTag);
        if (normalized > 1f)
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
