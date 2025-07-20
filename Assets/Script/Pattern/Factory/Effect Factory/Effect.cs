using System;
using UnityEngine;

public class FirstHit : Effect
{
    PlayerStateMachine playerStateMachine;
    InputReader inputReader;
    Targeter targeter;
    PlayerCombat playerCombat;
    public event Action OnFirstHit;

    public override string Name => "FirstHit";

    public override void Proccess(SpecialEffectsData effectsData, GameObject caster)
    {
        playerStateMachine = caster?.GetComponent<PlayerStateMachine>();
        inputReader = caster?.GetComponent<InputReader>();
        playerCombat = caster?.GetComponent<PlayerCombat>();
        targeter = caster?.GetComponentInChildren<Targeter>();
        Target target = targeter?.currentTarget;

        if (target != null)
        {
            OnFirstHit?.Invoke();
            if (target.isFirstAttack && inputReader.IsAttack)
            {
                playerStateMachine.SwitchState(new PlayerSpecialAttack(playerStateMachine, effectsData));
                return;
            }
        }
    }
}
