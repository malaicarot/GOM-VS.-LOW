using System;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    float previousFrameTime;
    bool alreadyApplyForce;
    Attack attack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AttackAnimationName, attack.AnimationDuration);
        foreach (AttackDealDamage attackDamage in stateMachine.AttackDealDamage)
        {
            attackDamage.SetAttack(attack.AttackDamage, attack.AttackKnockback);
        }
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttack)
            {
                TryCombo(normalizedTime);
            }
        }
        else
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

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    void TryCombo(float normalizedTime)
    {
        if (attack.AttackIndex == -1) { return; }
        if (normalizedTime < attack.AttackTime) { return; }

        stateMachine.SwitchState(
            new PlayerAttackState(
                stateMachine,
                attack.AttackIndex
            )
        );
    }

    void TryApplyForce()
    {
        if (alreadyApplyForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }
}
