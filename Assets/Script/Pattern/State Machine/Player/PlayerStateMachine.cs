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
    [field: SerializeField] public AnimatorOverrideController AnimatorOverrideController { get; private set; }
    [field: SerializeField] public AnimatorOverrideController AnimatorAttackOverrideController { get; private set; }
    [field: SerializeField] public AnimatorOverrideController AnimatorSecondaryAttackOverrideController { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public HealingPotion HealingPotion { get; private set; }
    [field: SerializeField] public Mana Mana { get; private set; }
    [field: SerializeField] public Stamina Stamina { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    // [field: SerializeField] public Attack[] Attacks { get; private set; }
    // [field: SerializeField] public Attack[] Attacks { get; private set; }

    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public AttackDealDamage[] AttackDealDamage { get; private set; }
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

    void Start()
    {
        CameraTransfrom = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    void OnEnable()
    {
        Health.OnTakeDamage += HandleAttack;
        Health.OnDeath += HandleDeadState;
        PlayerStats.IncreaseStats += SetStats;
        PlayerCombat.OnSetWeapon += SetAttackBaseWeapon;
    }

    void OnDisable()
    {
        Health.OnTakeDamage -= HandleAttack;
        Health.OnDeath -= HandleDeadState;
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

    public void OnJump()
    {
        SwitchState(new PlayerJumpState(this));
    }

    public void OnReturnFreeLook()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    public void SetStats()
    {
        // foreach (Attack attack in Attacks)
        // {
        //     attack.AttackDamage = PlayerStats.ReturnAttribute("Attacks");
        // }

        Health.SetResistance();
        Health.SetHealth();
    }
    public void SetAttackBaseWeapon()
    {
        Attacks = PlayerCombat.weapon.Attacks;
        AttacksSecondary = PlayerCombat.weaponSecondary.Attacks;
        AnimationClip atk_1 = PlayerCombat.weapon.AnimationClip[0];
        AnimationClip atk_2 = PlayerCombat.weapon.AnimationClip[1];
        AnimationClip atk_3 = PlayerCombat.weapon.AnimationClip[2];
        AnimationClip atk_4 = PlayerCombat.weapon.AnimationClip[3];


        AnimationClip atk_s_1 = PlayerCombat.weaponSecondary.AnimationClip[0];
        AnimationClip atk_s_2 = PlayerCombat.weaponSecondary.AnimationClip[1];


        SetAttackAnimation(atk_1, atk_2, atk_3, atk_4, atk_s_1, atk_s_2, AnimatorAttackOverrideController, Attacks, AttacksSecondary);
    }

    public void OnCastSkill()
    {
        if (Mana.currentMana <= 0) { return; }
        if (!PlayerSkill.playerSkillSingleton.ButtonOnClick(Buttons[InputReader.ButtonIndex])) { return; }

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

    public void PlayAnimation(AnimatorOverrideController animatorOverrideController, string overideName, int animationHash, AnimationClip animation)
    {
        AnimatorOverrideController runtimeOverride = new AnimatorOverrideController(animatorOverrideController);
        runtimeOverride[overideName] = animation;
        Animator.runtimeAnimatorController = runtimeOverride;
        Animator.CrossFadeInFixedTime(animationHash, CrossFadeDuration);
    }
    public void SetAttackAnimation(AnimationClip atk_1, AnimationClip atk_2, AnimationClip atk_3, AnimationClip atk_4, AnimationClip atk_s_1, AnimationClip atk_s_2, AnimatorOverrideController animatorOverrideController, Attack[] attack, Attack[] attack_secondary)
    {
        AnimatorOverrideController runtimeOverride = new AnimatorOverrideController(animatorOverrideController);
        runtimeOverride[attack[0].AttackAnimationName] = atk_1;
        runtimeOverride[attack[1].AttackAnimationName] = atk_2;
        runtimeOverride[attack[2].AttackAnimationName] = atk_3;
        runtimeOverride[attack[3].AttackAnimationName] = atk_4;

        runtimeOverride[attack_secondary[0].AttackAnimationName] = atk_s_1;
        runtimeOverride[attack_secondary[1].AttackAnimationName] = atk_s_2;
        Animator.runtimeAnimatorController = runtimeOverride;
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
}
