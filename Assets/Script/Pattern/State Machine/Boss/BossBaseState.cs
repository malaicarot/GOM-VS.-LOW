using UnityEngine;

public abstract class BossBaseState : State
{
    protected BossStateMachine bossStateMachine;
    float distanceSqr;
    float atkRangeSqr;
    float cautiousSqr;
    Vector3 direction;

    public BossBaseState(BossStateMachine bossStateMachine)
    {
        this.bossStateMachine = bossStateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        bossStateMachine.Controller.Move((motion + bossStateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        bossStateMachine.Controller.Move(Vector3.zero + bossStateMachine.ForceReceiver.Movement * deltaTime);
    }

    protected bool IsInChanseRange()
    {
        if (bossStateMachine.Player.isDead) { return false; }
        distanceSqr = (bossStateMachine.Player.transform.position - bossStateMachine.transform.position).sqrMagnitude;
        return distanceSqr <= bossStateMachine.BossChasingRange * bossStateMachine.BossChasingRange;
    }

    protected bool IsInAttackRange()
    {
        atkRangeSqr = (bossStateMachine.Player.transform.position - bossStateMachine.transform.position).sqrMagnitude;
        return atkRangeSqr <= bossStateMachine.BossAttackRange * bossStateMachine.BossAttackRange;
    }
    protected bool IsInCautiousRange()
    {
        if (bossStateMachine.Player.isDead) { return false; }
        cautiousSqr = (bossStateMachine.Player.transform.position - bossStateMachine.transform.position).sqrMagnitude;
        return cautiousSqr <= bossStateMachine.BossCautiousRange * bossStateMachine.BossCautiousRange;
    }



    protected void FaceTarget()
    {
        if (bossStateMachine.Player == null) { return; }
        direction = bossStateMachine.Player.transform.position - bossStateMachine.transform.position;
        direction.y = 0;
        bossStateMachine.transform.rotation = Quaternion.LookRotation(direction);
    }
}
