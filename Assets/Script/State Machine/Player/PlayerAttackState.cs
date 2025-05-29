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
            attackDamage.SetAttack(attack.AttackDamage);
        }
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime();

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

    float GetNormalizedTime()
    {
        AnimatorStateInfo currentState = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);


        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentState.IsTag("Attack"))
        {
            return currentState.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    void TryApplyForce()
    {
        if (alreadyApplyForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }
}
