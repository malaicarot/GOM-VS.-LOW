using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    readonly int EnemyAttackHash = Animator.StringToHash("Enemy_Attack");
    readonly string EnemyAttackTag = "Attack";

    public EnemyAttackState(EnemyStateMachine enemyState) : base(enemyState)
    {
    }

    public override void Enter()
    {
        enemyState.Animator.CrossFadeInFixedTime(EnemyAttackHash, enemyState.CrossFadeDuration);
        foreach (AttackDealDamage attackDamage in enemyState.AttackDealDamage)
        {
            attackDamage.SetAttack(enemyState.EnemyAttackDamage, enemyState.EnemyAttackKnockback);
        }
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        if (GetNormalizedTime(enemyState.Animator, EnemyAttackTag) >= 1f)
        {
            enemyState.SwitchState(new EnemyChasingState(enemyState));
        }
    }

    public override void Exit()
    {

    }
}
