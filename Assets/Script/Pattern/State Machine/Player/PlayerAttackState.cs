
public class PlayerAttackState : PlayerBaseState
{
    readonly string AttackAnimationTag = "Attack";
    float previousFrameTime;
    bool alreadyApplyForce;
    Attack attack;
    int index;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
        index = attackIndex;
    }

    public override void Enter()
    {
        stateMachine.Stamina.ReduceStamina(stateMachine.attackStaminaReduce);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AttackAnimationName, attack.AnimationDuration);

        foreach (AttackDealDamage attackDamage in stateMachine.AttackDealDamage)
        {
            attackDamage.SetAttack(stateMachine.PlayerStats.CalculateCritical(attack.AttackDamage), attack.AttackKnockback);
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, AttackAnimationTag);

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttack)
            {
                TryCombo(normalizedTime, "main");
                // if (stateMachine.InputReader.IsSecondaryAttack)
                // {
                //     TryCombo(normalizedTime, "secondary");
                // }
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

    void TryCombo(float normalizedTime, string attackType)
    {
        if (attack.AttackIndex == -1) { return; }
        if (normalizedTime < attack.AttackTime) { return; }

        // if (attackType == "main")
        // {
            stateMachine.SwitchState(
            new PlayerAttackState(
                stateMachine,
                attack.AttackIndex
            ));
        // }
        // else
        // {
        //     stateMachine.SwitchState(
        //     new PlayerSecondaryAttackState(
        //         stateMachine,
        //         stateMachine.AttacksSecondary[index].AttackIndex
        //     ));
        // }
    }

    void TryApplyForce()
    {
        if (alreadyApplyForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }
}
