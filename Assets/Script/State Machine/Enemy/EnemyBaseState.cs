using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine enemyState;
    float ditanceSqr;
    Vector3 direction;


    public EnemyBaseState(EnemyStateMachine enemyState)
    {
        this.enemyState = enemyState;
    }

    protected bool IsInChanseRange()
    {
        ditanceSqr = (enemyState.Player.transform.position - enemyState.transform.position).sqrMagnitude;
        return ditanceSqr <= enemyState.PlayerChasingRange * enemyState.PlayerChasingRange;
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
        direction = enemyState.Player.transform.position - enemyState.transform.position;
        enemyState.transform.rotation = Quaternion.LookRotation(direction);
    }
}
