using UnityEngine;

public class WalkState : StateMachine
{
    public WalkState(PlayerController _player) : base(_player) { }

    public override void Enter()
    {

    }

    public override void Exit()
    {
    }
    public override void Update()
    {
        player.ProcessMove(player.TarGetSpeed());
        animator.SetFloat(player.move_animation_blend_name, player.speed);
        if (player.CheckSprintInput())
        {
            player.SetState(new SprintState(this.player));
        }
        if (!player.CheckMoveInput())
        {
            player.SetState(new IdleState(this.player));
        }
        if (player.CheckJumpInput())
        {
            player.SetState(new JumpState(this.player));
        }
    }
}
