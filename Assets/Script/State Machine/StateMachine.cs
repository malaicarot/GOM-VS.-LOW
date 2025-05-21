
using UnityEngine;

public abstract class StateMachine
{
    protected PlayerController player;
    protected Animator animator;

    public StateMachine(PlayerController _player)
    {
        this.player = _player;
        this.animator = _player.animator;
    }


    public virtual void Enter() { }

    public virtual void Exit() { }

    public abstract void Update();
}
