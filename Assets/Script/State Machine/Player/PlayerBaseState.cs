using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected bool IsSprint()
    {
        return stateMachine.InputReader.IsSprint;
    }

    protected void Move(float deltaTime)
    {
        stateMachine.Controller.Move(Vector3.zero + stateMachine.ForceReceiver.Movement * deltaTime);
    }
    protected void FaceTarget()
    {
        if (stateMachine.Targeter.currentTarget == null) { return; }
        Vector3 direction = stateMachine.Targeter.currentTarget.transform.position - stateMachine.transform.position;
        direction.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(direction);
    }

    protected void ReturnToLocomotion()
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

    protected Vector3 CalculateDirection()
    {
        Vector3 forward = stateMachine.CameraTransfrom.forward;
        Vector3 right = stateMachine.CameraTransfrom.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        return forward * stateMachine.InputReader.Movement.y + right * stateMachine.InputReader.Movement.x;
    }
    
}
