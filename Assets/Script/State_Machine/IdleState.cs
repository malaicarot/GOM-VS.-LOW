using Unity.VisualScripting;
using UnityEngine;

public class IdleState : State_Machine
{
    public IdleState(PlayerController player) : base(player)
    { }

    bool lastCombatState;
    public override void Enter()
    {
        lastCombatState = player.OnCombatMode();

    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        SetAnimation();
        animator.SetFloat(player.move_animation_blend_name, player.TarGetSpeed());// Tính toán lại tốc độ

        if (player.CheckMoveInput())
        {
            player.SetState(new WalkState(this.player));
        }

        if (player.CheckJumpInput())
        {
            player.SetState(new JumpState(this.player));
        }

        if (player.CheckAttackInput())
        {
            player.SetState(new AttackState(this.player));
        }
    }

    void SetAnimation()
    {
        if (player.OnCombatMode() != lastCombatState)
        {
            if (player.OnCombatMode())
            {
                animator.SetTrigger("Combat_Mode");
            }
            else
            {
                animator.SetTrigger("Normal_Mode");

            }
            lastCombatState = player.OnCombatMode();
        }
    }
}
