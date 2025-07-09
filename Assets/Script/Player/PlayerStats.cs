using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Main Player Stats")]
    [SerializeField] int playerXP = 0;
    [SerializeField] int playerLevel = 0;
    [SerializeField] float playerHP = 100;


    [Header("Player Attributes")]
    public List<PlayerAttributes> playerAttributes = new List<PlayerAttributes>();


    [Header("Stats Increase")]
    [SerializeField] float manaIncrease = 10;
    [SerializeField] float staminaIncrease = 5;
    [SerializeField] float healthIncrease = 10;
    [SerializeField] int XPNeededToLevelUp = 10;

    // [Header("Event Action")]
    public event Action IncreaseStats;

    Health health;
    Mana mana;
    Stamina stamina;

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
            IncreaseStats?.Invoke();
            playerXP = playerXP - XPNeededToLevelUp;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        playerLevel++;
        XPNeededToLevelUp += 5;
        health.IncreaseHP(healthIncrease);
        mana.IncreaseMana(manaIncrease);
        stamina.IncreaseStamina(staminaIncrease);
    }
}
