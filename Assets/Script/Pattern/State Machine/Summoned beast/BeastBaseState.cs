using UnityEngine;

public abstract class BeastBaseState : State
{
    protected BeastStateMachine beastStateMachine;
    Vector3 direction;


    public BeastBaseState(BeastStateMachine beastStateMachine)
    {
        this.beastStateMachine = beastStateMachine;
    }


    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 nextPosition = beastStateMachine.Rigidbody.position + motion * deltaTime;
        beastStateMachine.Rigidbody.MovePosition(nextPosition);
    }

    protected void FaceTarget()
    {
        if (PlayerSkill.Instance.target == null) { return; }
        direction = PlayerSkill.Instance.target.transform.position - beastStateMachine.transform.position;

        direction.y = 0;
        beastStateMachine.transform.rotation = Quaternion.LookRotation(direction);
    }
}
