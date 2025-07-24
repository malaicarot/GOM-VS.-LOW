using System;
using UnityEngine;

public class FirstHit : Effect
{
    PlayerStateMachine playerStateMachine;
    InputReader inputReader;
    Targeter targeter;

    public override string Name => "FirstHit";

    public override void Proccess(SpecialEffectsData effectsData, GameObject caster)
    {
        playerStateMachine = caster?.GetComponent<PlayerStateMachine>();
        inputReader = caster?.GetComponent<InputReader>();
        targeter = caster?.GetComponentInChildren<Targeter>();
        Target target = targeter?.currentTarget;

        if (target != null)
        {
            Debug.Log("Target " + target.isFirstAttack);
            if (target.isFirstAttack && inputReader.IsAttack)
            {
                playerStateMachine.SwitchState(new PlayerSpecialAttack(playerStateMachine, effectsData));
                return;
            }
        }
    }
}
