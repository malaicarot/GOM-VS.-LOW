
using UnityEngine;

public abstract class State_Machine
{
    protected PlayerController player;
    protected Animator animator;

    public State_Machine(PlayerController _player)
    {
        this.player = _player;
        this.animator = _player.animator;
    }


    public virtual void Enter() { }

    public virtual void Exit() { }

    public abstract void FixedUpdate();
}
