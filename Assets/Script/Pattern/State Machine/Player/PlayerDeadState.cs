using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
    }

    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {

    }

    void ResSpawn()
    {
        stateMachine.Ragdoll.ToggleRagdoll(false);
        // stateMachine.transform.position = stateMachine.CheckPoint.position;
        stateMachine.Health.ResetHealth();
        stateMachine.Mana.ResetMana();
        stateMachine.Stamina.ResetStamina();
        stateMachine.HealingPotion.ResetPotion();
    }
}
