using System;
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
    [SerializeField] int XPNeededToLevelUp = 10;

    // [Header("Event Action")]
    public event Action IncreaseStats;

    Health health;
    Mana mana;
    Stamina stamina;
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
        playerLevel++;
        XPNeededToLevelUp += 5;
        increasePoint = playerLevel;
        IncreaseAttributes();
        health.SetHealth();
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
                default:
                    break;
            }
        }
    }
    void IncreaseAmount(string name)
    {
        foreach (BaseAttributes item in playerAttributes)
        {
            if (item.attributeData.name == name)
            {
                item.amount += attackDamageIncrease;
            }
        }
    }

    public void IncreaseAttackDamage()
    {
        IncreaseAmount("Attacks");
        IncreaseStats?.Invoke();
    }

    public int CalculateCritical(int currentDamage)
    {
        int criticalRate = UnityEngine.Random.Range(0, 99);
        if (criticalRate < ReturnAttribute("Critical"))
        {
            currentDamage += currentDamage / 2;
        }
        Debug.Log(currentDamage);
        return currentDamage;
    }

    public void IncreaseDefense()
    {

    }
    public void IncreaseIntrinsic()
    {

    }
}
