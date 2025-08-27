using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BeastStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public AttackDealDamage AttackDealDamage { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }



    public Health Enemy { get; private set; }

    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Boss").GetComponent<Health>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        SwitchState(new BeastAppearState(this));
    }

    void OnEnable()
    {
        AttackDealDamage.OnDissolve += HandleDisappear;
    }

    void OnDisable()
    {
        AttackDealDamage.OnDissolve -= HandleDisappear;
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
