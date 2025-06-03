using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine enemyState;
    float ditanceSqr;
    float atkRangeSqr;
    Vector3 direction;


    public EnemyBaseState(EnemyStateMachine enemyState)
    {
        this.enemyState = enemyState;
    }

    protected bool IsInChanseRange()
    {
        if(enemyState.Player.isDead){ return false; }
        ditanceSqr = (enemyState.Player.transform.position - enemyState.transform.position).sqrMagnitude;
        return ditanceSqr <= enemyState.EnemyChasingRange * enemyState.EnemyChasingRange;
    }

    protected bool IsInAttackRange()
    {
        atkRangeSqr = (enemyState.Player.transform.position - enemyState.transform.position).sqrMagnitude;
        return atkRangeSqr <= enemyState.EnemyAttackRange * enemyState.EnemyAttackRange;
    }

    protected void Move(float deltaTime)
    {
        enemyState.EnemyController.Move(Vector3.zero * deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        enemyState.EnemyController.Move((motion + enemyState.ForceReceiver.Movement) * enemyState.EnemySpeed * deltaTime);
    }

    protected void FaceTarget()
    {
        if (enemyState.Player == null) { return; }
        direction = enemyState.Player.transform.position - enemyState.transform.position;
        direction.y = 0;
        enemyState.transform.rotation = Quaternion.LookRotation(direction);
    }
}
