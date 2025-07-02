using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Button[] Buttons { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public AnimatorOverrideController AnimatorOverrideController { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public PlayerSkill PlayerSkill { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public HealingPotion HealingPotion { get; private set; }
    [field: SerializeField] public Mana Mana { get; private set; }
    [field: SerializeField] public Stamina Stamina { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public AttackDealDamage[] AttackDealDamage { get; private set; }
    [field: SerializeField] public Respawn Respawn { get; private set; }
    [field: SerializeField] public float FreeLookMoveSpeed { get; private set; }
    [field: SerializeField] public float TargetMoveSpeed { get; private set; }
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

    public Transform CameraTransfrom { get; private set; }
    void Start()
    {
        CameraTransfrom = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));

    }

    void OnEnable()
    {
        Health.OnTakeDamage += HandleAttack;
        Health.OnDeath += HandleDeadState;
    }
    void OnDisable()
    {
        Health.OnTakeDamage -= HandleAttack;
        Health.OnDeath -= HandleDeadState;
    }

    void HandleAttack()
    {
        SwitchState(new PlayerImpactState(this));
    }
    void HandleDeadState()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void OnJump()
    {
        SwitchState(new PlayerJumpState(this));
    }

    public void OnReturnFreeLook()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    // public void OnExitCheckPoint()
    // {
    //     SwitchState(new PlayerFreeLookState(this));
    //     DisableMoment();
    // }

    public void OnCastSkill()
    {
        if (Mana.currentMana <= 0) { return; }
        if (!PlayerSkill.ButtonOnClick(Buttons[InputReader.ButtonIndex])) { return; }

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
}
