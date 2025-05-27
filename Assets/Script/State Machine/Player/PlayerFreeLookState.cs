using JetBrains.Annotations;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{

    readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    const float AnimationDamping = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.Play(FreeLookBlendTreeHash);
        stateMachine.InputReader.TargetEvent += OnTarget;

    }
    public override void Tick(float deltaTime)
    {
        Vector3 direction = CalculateDirection();
        Move(direction * stateMachine.FreeLookMoveSpeed, deltaTime);

        if (stateMachine.InputReader.Movement == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(MovementSpeedHash, 0, AnimationDamping, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(MovementSpeedHash, 1, AnimationDamping, deltaTime);
        RotationByFaceDirection(direction, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    void RotationByFaceDirection(Vector3 direction, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(direction), stateMachine.RotationDamping * deltaTime);
    }

    Vector3 CalculateDirection()
    {
        Vector3 forward = stateMachine.CameraTransfrom.forward;
        Vector3 right = stateMachine.CameraTransfrom.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        return forward * stateMachine.InputReader.Movement.y + right * stateMachine.InputReader.Movement.x;
    }
    void OnTarget()
    {
        if (!stateMachine.Targeter.SelectedTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetState(stateMachine));
    }
}
