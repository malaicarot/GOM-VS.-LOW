using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public AttackDealDamage[] AttackDealDamage { get; private set; }
    [field: SerializeField] public CharacterController EnemyController { get; private set; }
    [field: SerializeField] public float EnemySpeed { get; private set; }
    [field: SerializeField] public int EnemyAttackDamage { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }
    [field: SerializeField] public float EnemyChasingRange { get; private set; }
    [field: SerializeField] public float EnemyAttackRange { get; private set; }
    [field: SerializeField] public float EnemyAttackKnockback { get; private set; }
    [field: SerializeField] public float TimeToDisappear { get; private set; }


    public Health Player { get; private set; }



    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        NavMeshAgent.updatePosition = false;
        NavMeshAgent.updateRotation = false;
        SwitchState(new EnemyIdleState(this));
    }

    void OnEnable()
    {
        Health.OnTakeDamage += HandleAttack;
        Health.OnDeath += HandleDeadState;
        FallToGround();
    }
    void OnDisable()
    {
        Health.OnTakeDamage -= HandleAttack;
        Health.OnDeath -= HandleDeadState;
    }

    void HandleAttack()
    {
        SwitchState(new EnemyImpactState(this));
    }

    void HandleDeadState()
    {
        SwitchState(new EnemyDeadState(this));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, EnemyChasingRange);
        Gizmos.DrawWireSphere(transform.position, EnemyAttackRange);
    }

    void FallToGround()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out raycastHit, 40f, LayerMask.GetMask("Ground")))
        {
            gameObject.transform.position = raycastHit.point;
        }

    }

    public void ReturnEnemy()
    {
        StartCoroutine(WaitToReturnEnemy());
    }

    IEnumerator WaitToReturnEnemy()
    {
        yield return new WaitForSeconds(TimeToDisappear);
        this.GetComponent<PooledObject>()?.Release();
    }
}
