using UnityEngine;

public class JumpState : StateMachine
{
    public JumpState(PlayerController _player) : base(_player) { }
    float jumpTime = 2f;

    public override void Enter()
    {
        animator.SetTrigger("Jump");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        player.SetState(new SprintState(this.player));
        
        if (!CountdownTime.SingletonCountdown.Countdown(jumpTime))
        {

            player.SetState(new IdleState(this.player));
        }
    }
}
