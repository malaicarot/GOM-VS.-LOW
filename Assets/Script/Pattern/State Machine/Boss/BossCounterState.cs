using UnityEngine;

public class BossCounterState : BossBaseState
{
    readonly int Counter_1Hash = Animator.StringToHash("Counter_1");
    readonly int Counter_2Hash = Animator.StringToHash("Counter_2");
    readonly string EnemyCounterTag = "Counter";

    public BossCounterState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        bossStateMachine.WeaponDisalbe.gameObject.SetActive(false);
        bossStateMachine.WeaponThrow.gameObject.SetActive(true);
        bossStateMachine.AttackHandler.OnEnableSpecialAttack();
        bossStateMachine.Animator.CrossFadeInFixedTime(Counter_1Hash, bossStateMachine.CrossFadeDuration);
        bossStateMachine.hitCount = 0;
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();

        Counter();
        ThrowWeaponToPlayer(deltaTime);
    }

    public override void Exit()
    {

    }

    void Counter()
    {
        bossStateMachine.hitCount = 0;
        if (GetNormalizedTime(bossStateMachine.Animator, EnemyCounterTag) > 0.8f && GetNormalizedTime(bossStateMachine.Animator, EnemyCounterTag) < 1f)
        {
            bossStateMachine.WeaponDisalbe.gameObject.SetActive(true);
            bossStateMachine.WeaponThrow.gameObject.SetActive(false);
            bossStateMachine.AttackHandler.OnDisableSpecialAttack();

            bossStateMachine.SwitchState(new BossIdleState(bossStateMachine));
        }
    }

    void ThrowWeaponToPlayer(float deltaTime)
    {
        Vector3 nomarlizedDirection = (bossStateMachine.Player.transform.position - bossStateMachine.WeaponThrow.transform.position).normalized;
        bossStateMachine.WeaponThrow.transform.position += nomarlizedDirection * bossStateMachine.WeaponThrowSpeed * deltaTime;
    }
}
