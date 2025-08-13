using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Attack[] Attack { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public BossSkill BossSkill { get; private set; }
    [field: SerializeField] public AttackDealDamage[] AttackDealDamage { get; private set; }
    [field: SerializeField] public AttackHandler AttackHandler { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public GameObject WeaponThrow { get; set; }
    [field: SerializeField] public GameObject WeaponDisalbe { get; set; }
    [field: SerializeField] public Transform Projectile { get; set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float DashSpeed { get; private set; }
    [field: SerializeField] public float BulletForce { get; private set; }
    [field: SerializeField] public float WeaponThrowSpeed { get; private set; }
    [field: SerializeField] public float BossJumpForce { get; private set; }
    [field: SerializeField] public float FallMultiplier { get; private set; }
    [field: SerializeField] public int BossAttackDamage { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }
    [field: SerializeField] public float BossChasingRange { get; private set; }
    [field: SerializeField] public float BossAttackRange { get; private set; }
    [field: SerializeField] public float BossAttackKnockback { get; private set; }
    [field: SerializeField] public float TimeResetInterruped { get; private set; }
    // [field: SerializeField] public float TimeResetInterruped { get; private set; }

    [field: SerializeField] public int JumpAttackTime { get; private set; }
    [field: SerializeField] public int[] RandomHitCount { get; private set; }


    public Health Player { get; private set; }
    public Attack[] AttackRandom { get; set; }
    public int hitCount { get; set; }
    public int countJumpAttack { get; set; } = 0;
    public int countBallisticsAttack { get; set; } = 0;



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
        Health.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        Health.OnTakeDamage -= HandleAttack;
        Health.OnDeath -= HandleDeath;
    }

    void HandleAttack()
    {
        SwitchState(new BossImpactState(this));
    }

    void HandleDeath()
    {
        SwitchState(new BossDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, BossChasingRange);
        Gizmos.DrawWireSphere(transform.position, BossAttackRange);
    }


    public void RandomCombo()
    {
        List<int> validIndex = new List<int>();
        int validNumber = Attack.Length;
        AttackRandom = new Attack[validNumber];
        for (int i = 0; i < AttackRandom.Length; i++)
        {
            int index = Random.Range(0, validNumber);
            while (validIndex.Contains(index))
            {
                index = Random.Range(0, validNumber);
            }
            validIndex.Add(index);
            AttackRandom[i] = Attack[index];
        }

        for (int i = 0; i < AttackRandom.Length; i++)
        {
            if (i == AttackRandom.Length - 1)
            {
                AttackRandom[i].AttackIndex = -1;
                AttackRandom[i].AttackTime = 0;
                break;
            }
            AttackRandom[i].AttackIndex = i + 1;
            AttackRandom[i].AttackTime = 0.8f;
        }
    }

    public int RandomHitCountToCounter()
    {
        int index = Random.Range(0, RandomHitCount.Length);
        Debug.Log("Hit Count Random: " + RandomHitCount[index]);
        return RandomHitCount[index];
    }


    public void UseSkill(string _name, SkillData skillData, Transform spawn)
    {
        string name = _name;
        Ability ability = AbilityFactory.GetAbility(name);
        if (ability != null)
        {
            ability.Proccess(skillData, this.gameObject, spawn);
        }
    }

}
