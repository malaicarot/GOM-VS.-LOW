using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BeastStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public AttackDealDamage AttackDealDamage { get; private set; }
    [field: SerializeField] public GameObject Effect { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }
    [field: SerializeField] public bool IsMove { get; private set; }

    void OnEnable()
    {
        if (AttackDealDamage != null)
        {
            AttackDealDamage.OnDissolve += HandleDisappear;
        }
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        SwitchState(new BeastAppearState(this));
    }


    void OnDisable()
    {
        if (AttackDealDamage != null)
        {
            AttackDealDamage.OnDissolve -= HandleDisappear;
        }
    }

    void HandleDisappear()
    {
        SwitchState(new BeastDisappearState(this));
    }

    public void ReturnBeast()
    {
        StartCoroutine(WaitToReturnBeast());
    }

    IEnumerator WaitToReturnBeast()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<PooledObject>()?.Release();
    }
}
