using System;
using UnityEngine;

public class AttackState : State_Machine
{
    public AttackState(PlayerController player) : base(player)
    { }

    int hashAttack = Animator.StringToHash("Light_Attack");
    float maxComboTime = 3f;
    float lastClicked = 0;
    float comboTime = 0;

    public override void Enter()
    {
        OnAttackAnimation();
    }

    public override void Exit()
    {
        animator.SetInteger(hashAttack, 0);
    }
    public override void FixedUpdate()
    {
        comboTime += Time.time;
        if (comboTime > maxComboTime)
        {
            player.SetState(new IdleState(this.player));
        }
        OnAttackAnimation();


    }

    void OnAttackAnimation()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger(hashAttack, player.ComboAttackCount());
        // comboTime -= Time.time;
    }

}
