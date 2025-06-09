using System;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    readonly string EnemyAttackTag = "Attack";
    Attack attack;
    float previousFrameTime;
    bool alreadyApplyForce;


    public BossAttackState(BossStateMachine bossStateMachine, int attackIndex) : base(bossStateMachine)
    {
        attack = bossStateMachine.Attack[attackIndex];
    }

    public override void Enter()
    {
        bossStateMachine.Animator.CrossFadeInFixedTime(attack.AttackAnimationName, attack.AnimationDuration);
        foreach (AttackDealDamage attackDealDamage in bossStateMachine.AttackDealDamage)
        {
            attackDealDamage.SetAttack(attack.AttackDamage, attack.AttackKnockback);
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(bossStateMachine.Animator, EnemyAttackTag);
        if (normalizedTime >= previousFrameTime && normalizedTime <= 1f && IsInAttackRange())
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();

            }

            TryCombo(normalizedTime);
        }
        else
        {
            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
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

        bossStateMachine.SwitchState(
            new BossAttackState(
                bossStateMachine,
                attack.AttackIndex
            )
        );
    }

    void TryApplyForce()
    {
        if (alreadyApplyForce) { return; }
        bossStateMachine.ForceReceiver.AddForce(bossStateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }


}
