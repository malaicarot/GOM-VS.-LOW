using UnityEngine;

public class PlayerCastSkillState : PlayerBaseState
{
    public PlayerCastSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.PlayerSkill.UseSkill(stateMachine.InputReader.ButtonIndex);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}
