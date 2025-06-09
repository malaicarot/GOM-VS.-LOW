using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Attack[] Attack { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public AttackDealDamage[] AttackDealDamage { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public int BossAttackDamage { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }
    [field: SerializeField] public float BossChasingRange { get; private set; }
    [field: SerializeField] public float BossAttackRange { get; private set; }
    [field: SerializeField] public float BossAttackKnockback { get; private set; }
    public Health Player { get; private set; }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        SwitchState(new BossIdleState(this));
    }

    void OnEnable()
    {
        Health.OnTakeDamage += HandleAttack;
    }

    void OnDisable()
    {
        Health.OnTakeDamage -= HandleAttack;

    }
    void HandleAttack()
    {
        SwitchState(new BossImpactState(this));
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, BossChasingRange);
        Gizmos.DrawWireSphere(transform.position, BossAttackRange);
    }

}
