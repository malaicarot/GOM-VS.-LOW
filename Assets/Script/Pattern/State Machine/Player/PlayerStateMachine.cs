using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public PlayerCombat PlayerCombat { get; private set; }
    [field: SerializeField] public Button[] Buttons { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GameObject SkillPosition { get; private set; }
    [field: SerializeField] public GameObject HighSkillPosition { get; private set; }
    [field: SerializeField] public GameObject SummonPosition { get; private set; }
    [field: SerializeField] public Transform Enhancement { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public HealingPotion HealingPotion { get; private set; }
    [field: SerializeField] public Mana Mana { get; private set; }
    [field: SerializeField] public Stamina Stamina { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public AttackDealDamage[] AttackDealDamage { get; private set; }
    [field: SerializeField] public AttackHandler AttackHandlers { get; private set; }
    [field: SerializeField] public DodgeController DodgeController { get; private set; }
    [field: SerializeField] public Respawn Respawn { get; private set; }
    [field: SerializeField] public float FreeLookMoveSpeed { get; private set; }
    [field: SerializeField] public float TargetMoveSpeed { get; private set; }
    [field: SerializeField] public float LowStaminaSpeed { get; private set; }

    [field: SerializeField] public float MultiplyCoefficientSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float CrossFadeDuration { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float sprintStaminaReduce { get; private set; }
    [field: SerializeField] public float jumpStaminaReduce { get; private set; }
    [field: SerializeField] public float attackStaminaReduce { get; private set; }
    [field: SerializeField] public float dodgeStaminaReduce { get; private set; }
    [field: SerializeField] public float healing { get; private set; }
    [field: SerializeField] public float reducePotion { get; private set; }
    [field: SerializeField] public float staminaRecovery { get; private set; }
    [field: SerializeField] public int attackDamageUp { get; private set; }

    public Transform CameraTransfrom { get; private set; }

    public Attack[] Attacks { get; private set; }
    public Attack[] AttacksSecondary { get; private set; }
    public AttackHandler AttackHandlerEnemy { get; set; }
    public float perfectDodgeTime;

    void Start()
    {
        CameraTransfrom = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    void OnEnable()
    {
        Health.OnTakeDamage += HandleAttack;
        Health.OnDeath += HandleDeadState;
        Health.OnStun += HandleStunState;
        PlayerStats.IncreaseStats += SetStats;
        PlayerCombat.OnSetWeapon += SetAttackBaseWeapon;
    }

    void OnDisable()
    {
        Health.OnTakeDamage -= HandleAttack;
        Health.OnDeath -= HandleDeadState;
        Health.OnStun -= HandleStunState;
        PlayerStats.IncreaseStats -= SetStats;
        PlayerCombat.OnSetWeapon -= SetAttackBaseWeapon;

    }

    void HandleAttack()
    {
        SwitchState(new PlayerImpactState(this));
    }

    void HandleDeadState()
    {
        SwitchState(new PlayerDeadState(this));
    }

    void HandleStunState()
    {
        SwitchState(new PlayerStunState(this));
    }

    public void OnJump()
    {
        SwitchState(new PlayerJumpState(this));
    }

    public void OnReturnFreeLook()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
    public void OnDodge()
    {

        SwitchState(new PlayerDodgingState(this, this.InputReader.Movement));
    }

    public void NonInterrupted()
    {
        InputReader.DodgeEvent -= OnDodge;
        InputReader.JumpEvent -= OnJump;
        // InputReader.IsAttack = false;
    }

    public void SetStats()
    {
        // foreach (Attack attack in Attacks)
        // {
        //     attack.AttackDamage = PlayerStats.ReturnAttribute("Attacks");
        // }

        Health.SetResistance();
        Health.SetHealth();
        Stamina.SetStamina();
    }
    public void SetAttackBaseWeapon()
    {
        Attacks = PlayerCombat.weapon.Attacks;
        AttacksSecondary = PlayerCombat.weaponSecondary.Attacks;
    }

    public void OnCastSkill()
    {
        if (Mana.currentMana <= 0) { return; }
        if (!PlayerSkill.Instance.ButtonOnClick(Buttons[InputReader.ButtonIndex])) { return; }

        Buttons[InputReader.ButtonIndex].onClick.Invoke();
        SwitchState(new PlayerCastSkillState(this));
    }

    public Transform GetTransformFromCheckPoint(Transform transform)
    {
        return transform;
    }

    public void HandleHealing()
    {
        SwitchState(new PlayerHealingState(this));
    }

    void OnTriggerEnter(Collider other)
    {
        if (InputReader.IsSprint)
        {
            if (other.CompareTag("Ledge"))
            {
                SwitchState(new PlayerLedgeBalanceState(this));
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (InputReader.IsInteract)
        {
            if (other.CompareTag("Item"))
            {
                SwitchState(new PlayerCollectionState(this));
            }
            else if (other.CompareTag("CheckPoint"))
            {
                CheckPoint checkPoint = other.GetComponent<CheckPoint>();
                checkPoint.GlowingEyes();
                GetTransformFromCheckPoint(other.gameObject.transform);
                Respawn.respawnTransform = other.gameObject.transform;
                SwitchState(new PlayerGetCheckPointState(this));
            }
        }
    }
    public void DisableMoment()
    {
        InputReader.cursorLocked = !InputReader.cursorLocked;
        InputReader.cursorInputForLook = !InputReader.cursorInputForLook;
        InputReader.OnApplicationFocus(InputReader.cursorLocked);
    }

    public void Rest()
    {
        Health.ResetHealth();
        Mana.ResetMana();
        Stamina.ResetStamina();
        HealingPotion.ResetPotion();
    }

    IEnumerator SlowMotion(float duration, float slowFactor)
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * slowFactor;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void StartSlowMotion(float duration, float slowFactor)
    {
        StartCoroutine(SlowMotion(duration, slowFactor));
    }
}
