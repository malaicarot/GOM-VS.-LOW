using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Main Player Stats")]
    [SerializeField] int playerXP = 0;
    [SerializeField] int playerLevel = 0;

    [Header("Player Attributes")]
    public List<BaseAttributes> playerAttributes = new List<BaseAttributes>();


    [Header("Stats Increase")]
    [SerializeField] int manaIncrease = 10;
    [SerializeField] int staminaIncrease = 5;
    [SerializeField] int damageIncrease = 10;
    [SerializeField] int healthIncrease = 10;
    [SerializeField] int attackDamageIncrease = 10;
    [SerializeField] int resistanceIncrease = 10;
    [SerializeField] int criticalIncrease = 10;
    [SerializeField] int XPNeededToLevelUp = 10;

    // [Header("Event Action")]
    public event Action IncreaseStats;

    Health health;
    Mana mana;
    Stamina stamina;
    int criticalRate = 10;
    int increasePoint = 0;

    void Start()
    {
        health = GetComponent<Health>();
        mana = GetComponent<Mana>();
        stamina = GetComponent<Stamina>();
    }

    public void RaiseXP(int amount)
    {
        playerXP += amount;
        if (playerXP >= XPNeededToLevelUp)
        {
            LevelUp();
            playerXP = playerXP - XPNeededToLevelUp;
            IncreaseStats?.Invoke();
        }
    }

    public void LevelUp()
    {
        Effect effect = EffectFactory.GetEffect("LevelUp");
        effect.Proccess(gameObject);
        playerLevel++;
        XPNeededToLevelUp += 5;
        increasePoint = playerLevel;
        IncreaseAttributes();
        health.SetHealth();
        health.SetResistance();
        mana.SetMana();
        stamina.SetStamina();
    }

    public int ReturnAttribute(string name)
    {
        foreach (BaseAttributes item in playerAttributes)
        {
            if (item.attributeData.name == name)
            {
                return item.amount;
            }
        }
        return 0;
    }

    public Sprite ReturnAttributeSprite(string name)
    {
        foreach (BaseAttributes item in playerAttributes)
        {
            if (item.attributeData.name == name)
            {
                return item.attributeData.Thumbnail;
            }
        }
        return null;
    }

    public void IncreaseAttributes()
    {
        foreach (BaseAttributes item in playerAttributes)
        {
            switch (item.attributeData.name)
            {
                case "Health":
                    item.amount += healthIncrease;
                    break;
                case "Mana":
                    item.amount += manaIncrease;
                    break;
                case "Stamina":
                    item.amount += staminaIncrease;
                    break;
                case "Attacks":
                    item.amount += attackDamageIncrease;
                    break;
                case "Resistance":
                    item.amount += resistanceIncrease;
                    break;
                case "Critical":
                    item.amount += criticalIncrease;
                    break;
                default:
                    break;
            }
        }
    }
    void IncreaseAmount(string name, int amount)
    {
        foreach (BaseAttributes item in playerAttributes)
        {
            if (item.attributeData.name == name)
            {
                item.amount += amount;
            }
        }
    }

    public void IncreaseAttackDamage()
    {
        IncreaseAmount("Attacks", attackDamageIncrease);
        IncreaseStats?.Invoke();
    }
    public void IncreaseCritical(int amount)
    {
        IncreaseAmount("Critical", amount);
        IncreaseStats?.Invoke();
    }
    public void IncreaseResistance(int amount)
    {
        IncreaseAmount("Resistance", amount);
        IncreaseStats?.Invoke();
    }

    public void IncreaseIntrinsic()
    {
        IncreaseAmount("Health", healthIncrease);
        IncreaseStats?.Invoke();
    }
    public void IncreaseStamina(int amount)
    {
        IncreaseAmount("Stamina", amount);
        IncreaseStats?.Invoke();
    }

    public int CalculateCritical(int currentDamage)
    {
        int criticalRate = UnityEngine.Random.Range(0, 99);
        if (criticalRate < ReturnAttribute("Critical"))
        {
            currentDamage += currentDamage / 2;
        }
        return currentDamage;
    }


    /*Resistance*/
    public void ReturnResistanceValue(int amount)
    {
        StartCoroutine(ReturnDefaultResistanceValue(amount));
    }

    IEnumerator ReturnDefaultResistanceValue(int amount)
    {
        IncreaseResistance(amount);
        yield return new WaitForSecondsRealtime(10f);
        IncreaseResistance(-amount);
    }

    /*Stamina*/
    public void ReturnStaminaValue(int amount)
    {
        StartCoroutine(ReturnDefaultStaminaValue(amount));
    }

    IEnumerator ReturnDefaultStaminaValue(int amount)
    {
        IncreaseStamina(amount);
        yield return new WaitForSecondsRealtime(10f);
        IncreaseStamina(-amount);
    }

    /*Critical*/
    public void ReturnCriticalValue(int amount)
    {
        StartCoroutine(ReturnDefaultCriticalValue(amount));
    }

    IEnumerator ReturnDefaultCriticalValue(int amount)
    {
        IncreaseCritical(amount);
        yield return new WaitForSecondsRealtime(10f);
        IncreaseCritical(-amount);
    }

    public void Footstep()
    {
        SoundManager.Instance.PlaySFX("Footstep");
    }

    public void JumpStart()
    {
        SoundManager.Instance.PlaySFX("JumpStart");
    }

    public void Grounded()
    {
        SoundManager.Instance.PlaySFX("Grounded");
    }
}
