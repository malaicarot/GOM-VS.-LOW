using System;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    float previousFrameTime;
    Attack attack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AttackAnimationName, attack.AnimationDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime > previousFrameTime && normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttack)
            {

                TryCombo(normalizedTime);
            }
        }
        else
        {
            //Back to Locomotion
        }



        previousFrameTime = normalizedTime;
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

    public override void Exit()
    {

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
}
