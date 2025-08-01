
public class PlayerAttackState : PlayerBaseState
{
    readonly string AttackAnimationTag = "Attack";
    float previousFrameTime;
    bool alreadyApplyForce;
    Attack attack;
    int currentAttackIndex;
    Attack[] currentComboList;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex, Attack[] comboList) : base(stateMachine)
    {
        currentComboList = comboList;
        currentAttackIndex = attackIndex;
        if (attackIndex >= comboList.Length)
        {
            currentAttackIndex = 0;
        }
        attack = currentComboList[currentAttackIndex];
    }

    public override void Enter()
    {
        stateMachine.AttackHandlers.PlayAttackSound += PlayerSound;

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
                TryCombo(normalizedTime, attack.AttackIndex, stateMachine.Attacks);
                stateMachine.InputReader.IsAttack = false;
            }

            if (stateMachine.InputReader.IsSecondaryAttack)
            {
                TryCombo(normalizedTime, attack.AttackIndex, stateMachine.AttacksSecondary);
                stateMachine.InputReader.IsSecondaryAttack = false;
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
        stateMachine.AttackHandlers.PlayAttackSound -= PlayerSound;

    }

    void TryCombo(float normalizedTime, int attackIndex, Attack[] nextComboList)
    {
        if (attack.AttackIndex == -1) { return; }
        if (normalizedTime < attack.AttackTime) { return; }


        stateMachine.SwitchState(
        new PlayerAttackState(
            stateMachine,
            attackIndex,
            nextComboList
        ));
    }

    void TryApplyForce()
    {
        if (alreadyApplyForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }

    void PlayerSound()
    {
        SoundManager.soundManager.PlaySound(attack.AudioClip);
    }
}
