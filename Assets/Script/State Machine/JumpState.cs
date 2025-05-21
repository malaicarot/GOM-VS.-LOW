using UnityEngine;

public class JumpState : StateMachine
{
    public JumpState(PlayerController _player) : base(_player) { }
    float jumpTime = 2f;

    public override void Enter()
    {
        Debug.Log("JumpState");
        animator.SetTrigger("Jump");
    }

    public override void Exit()
    {
        animator.SetBool("Jump", false);

    }

    public override void Update()
    {
        if (CountdownTime.SingletonCountdown.Countdown(jumpTime))
        {
            player.SetState(new SprintState(this.player));
        }

        player.SetState(new IdleState(this.player));
    }
}
