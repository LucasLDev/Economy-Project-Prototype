using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Space]

    [Header("Player")]
    public int playerMaxHealth = 5;
    public int playerCurrentHealth;
    public Slider playerHealthBar;
    public int playerMoveSpeed = 4;
    public int playerDamage = 1;
    public float projectileSpeed = 20f;
    public bool inSafeZone;

    [Space]

    public int medkitPotency;

    [Space]
    
    [Header("Zombies")]
    public int numberOfZombies;
    public int zombieMaxHealth = 5;
    public int zombieCurrentHealth;
    public Slider ZombieHealthBar;
    public int zombieDamage = 1;
    public int zombieSpeed = 1;
    public int zombieChaseRange;
    public float waitTime;
    public float startWaitTime;
    public bool zombieChasing;
    public bool zombiePatrolling;
    public bool zombiesDead;
    public bool zombiesSpawned;

    [Space]

    public int currentFuel;
    public int fuelGain;
    public TMP_Text fuelAmount;

    [Space]

    [Header("Store")]

    [Space]

    public bool storeEnabled;

    [Space]

    public int healthCost = 50;
    public int damageCost = 100;
    public int speedCost = 150;
    public int projectileCost = 25;
    public int fuelCost = 75;

    [Space]

    public int maxUpgradedHealth = 15;
    public int maxUpgradedDamage = 5;
    public int maxUpgradedSpeed = 10;
    public int maxUpgradedProjectile = 40;
    public int maxUpgradedFuel = 10;

    [Space]
    
    public TMP_Text healthStat;
    public TMP_Text dmgStat;
    public TMP_Text speedStat;
    public TMP_Text projectileSpeedStat;
    public TMP_Text FuelGainStat;

    [Space]

    public TMP_Text healthCostText;
    public TMP_Text damageCostText;
    public TMP_Text speedCostText;
    public TMP_Text projectileCostText;
    public TMP_Text fuelCostText;


    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        playerHealthBar.maxValue = playerMaxHealth;

        zombieMaxHealth += playerMaxHealth * 5/10;
        zombieCurrentHealth = zombieMaxHealth;
        ZombieHealthBar.maxValue = zombieMaxHealth;

        storeEnabled = false;
    }

    void Update()
    {
        playerHealthBar.value = playerCurrentHealth;
        ZombieHealthBar.value = zombieCurrentHealth;

        fuelAmount.SetText("Fuel:" + currentFuel);

        healthStat.SetText("" + playerMaxHealth);
        dmgStat.SetText("" + playerDamage);
        speedStat.SetText("" + playerMoveSpeed);
        projectileSpeedStat.SetText("" + projectileSpeed);
        FuelGainStat.SetText("" + fuelGain);

        healthCostText.SetText("" + healthCost);
        damageCostText.SetText("" + damageCost);
        speedCostText.SetText("" + speedCost);
        projectileCostText.SetText("" + projectileCost);
        fuelCostText.SetText("" + fuelCost);
    }
   
}
