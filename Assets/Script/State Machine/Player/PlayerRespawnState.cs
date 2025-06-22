using UnityEngine;

public class PlayerRespawnState : PlayerBaseState
{
    public PlayerRespawnState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(false);
        stateMachine.transform.position = stateMachine.CheckPoint.position;
        stateMachine.Health.ResetHealth();
        stateMachine.Mana.ResetMana();
        stateMachine.Stamina.ResetStamina();
        stateMachine.HealingPotion.ResetPotion();

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.HandleReturnFreeLookState();

    }
    public override void Exit()
    {

    }
}
