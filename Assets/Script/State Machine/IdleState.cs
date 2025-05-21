using Unity.VisualScripting;
using UnityEngine;

public class IdleState : StateMachine
{
    public IdleState(PlayerController player) : base(player)
    { }
    public override void Enter()
    {
        Debug.Log("Idle");
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
    }

    public override void Update()
    {
        animator.SetFloat(player.move_animation_blend_name, player.TarGetSpeed()); // Tính toán lại tốc độ
        if (player.CheckMoveInput())
        {
            player.SetState(new WalkState(this.player));
        }

        if (player.CheckJumpInput())
        {
            player.SetState(new JumpState(this.player));
        }
    }
}
