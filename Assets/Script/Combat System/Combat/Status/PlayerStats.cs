using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Main Player Stats")]
    [SerializeField] AttributeData playerXP;
    [SerializeField] AttributeData playerLevel;
    public AttributeData skillUpPoint;

    [Header("Player Attributes")]
    public List<AttributeData> AttributeDatas;

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
        playerXP.Value += amount;
        if (playerXP.Value >= XPNeededToLevelUp)
        {
            LevelUp();
            playerXP.Value -= -XPNeededToLevelUp;
            IncreaseStats?.Invoke();
        }
    }

    public void LevelUp()
    {
        Effect effect = EffectFactory.GetEffect("LevelUp");
        effect.Proccess(gameObject);
        skillUpPoint.Value++;
        playerLevel.Value++;
        XPNeededToLevelUp += 5;
        increasePoint = playerLevel.Value;
        IncreaseAttributes();
        health.SetHealth();
        health.SetResistance();
        mana.SetMana();
        stamina.SetStamina();
        UIManagers.Instance.SetUpStats();
    }

    public int ReturnAttribute(string name)
    {
        foreach (AttributeData stat in AttributeDatas)
        {
            if (stat.name == name)
            {
                return stat.Value;
            }
        }
        return 0;
    }

    public Sprite ReturnAttributeSprite(string name)
    {
        foreach (AttributeData stat in AttributeDatas)
        {
            if (stat.name == name)
            {
                return stat.Thumbnail;
            }
        }
        return null;
    }

    public void IncreaseAttributes()
    {
        foreach (AttributeData stat in AttributeDatas)
        {
            switch (stat.name)
            {
                case "Health":
                    stat.Value += healthIncrease;
                    break;
                case "Mana":
                    stat.Value += manaIncrease;
                    break;
                case "Stamina":
                    stat.Value += staminaIncrease;
                    break;
                case "Attacks":
                    stat.Value += attackDamageIncrease;
                    break;
                case "Resistance":
                    stat.Value += resistanceIncrease;
                    break;
                case "Critical":
                    stat.Value += criticalIncrease;
                    break;
                default:
                    break;
            }
        }
    }

    void IncreaseAmount(string name, int amount)
    {
        foreach (AttributeData stat in AttributeDatas)
        {
            if (stat.name == name)
            {
                stat.Value += amount;
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

}
