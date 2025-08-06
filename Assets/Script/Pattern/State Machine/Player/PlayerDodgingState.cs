using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    readonly int PerfectDodgeHash = Animator.StringToHash("PerfectDodge");
    readonly int DodgeForwardHash = Animator.StringToHash("Dodge_Forward");
    readonly int DodgeRightHash = Animator.StringToHash("Dodge_Right");
    Vector3 dodgingDirection;
    float dodgeRemainingTime;
    float duration = 0.5f;
    float slowFactor = 0.5f;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirection) : base(stateMachine)
    {
        if (dodgingDirection == Vector3.zero)
        {
            this.dodgingDirection.y = -1; // default dodge backward
        }
        else
        {
            this.dodgingDirection = dodgingDirection;
        }
    }

    public override void Enter()
    {
        Dodge();
        stateMachine.Stamina.ReduceStamina(stateMachine.dodgeStaminaReduce);
    }

    public override void Tick(float deltaTime)
    {


        Vector3 targetMovement = new Vector3();

        targetMovement += stateMachine.transform.right * dodgingDirection.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        targetMovement += stateMachine.transform.forward * dodgingDirection.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        Move(targetMovement, deltaTime);
        FaceTarget();

        dodgeRemainingTime -= deltaTime;
        if (dodgeRemainingTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetState(stateMachine));
        }

    }

    public override void Exit()
    {
        stateMachine.Health.SetParry(false);
    }

    void Dodge()
    {
        if (stateMachine.DodgeController.isPerfect)
        {
            Debug.Log("Perfect Dodge!");
            stateMachine.StartSlowMotion(duration, slowFactor);
            stateMachine.Health.SetParry(true);

            // stateMachine.Animator.CrossFadeInFixedTime(PerfectDodgeHash, stateMachine.CrossFadeDuration);
            // stateMachine.Health.SetParry(true);
            // dodgeRemainingTime = 1f;
            stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, stateMachine.CrossFadeDuration);
            stateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirection.y);
            stateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirection.x);
            dodgeRemainingTime = stateMachine.DodgeDuration;
        }
        else
        {

            stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, stateMachine.CrossFadeDuration);
            stateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirection.y);
            stateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirection.x);
            dodgeRemainingTime = stateMachine.DodgeDuration;
        }
    }
}
