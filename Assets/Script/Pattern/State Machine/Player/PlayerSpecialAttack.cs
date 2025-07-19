using UnityEngine;

public class PlayerSpecialAttack : PlayerBaseState
{
    readonly int SpecialAttackHash = Animator.StringToHash("SpecialAttack");
    readonly string SpecialAttackTag = "SpecialAttack";
    public PlayerSpecialAttack(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.PlayAnimation(SpecialAttackHash, SpecialEffectManagers.specialEffectManagers.FirstHitAnimation());
        Debug.Log("First Hit");
    }

    public override void Tick(float deltaTime)
    {
        float normalized = GetNormalizedTime(stateMachine.Animator, SpecialAttackTag);
        if (normalized > 1f)
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
    }


    public override void Exit()
    {
    }

}
