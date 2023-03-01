using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Space]

    public bool gameIsPaused = false;
    public bool storeEnabled = false;
    public bool favourCompleted = false;
    public bool inDialogue = false;
    public bool canShoot = true;

    public GameObject HUD;
    public StoreMenu store;

    [Space]

    [Header("Player")]
    public int playerMaxHealth = 5;
    public float playerCurrentHealth;
    public float playerMoveSpeed = 4;
    public float playerDamage = 1;
    public float projectileSpeed = 20f;
    public int medkitPotency;
    public bool inSafeZone;
    public bool canMove;

    [Header("Level System")]
    public int level;
    public float currentXp;
    public float requiredXp;
    public float testXP;

    private float lerpTimer;
    private float delayTimer;
    public float chipSpeed = 4f;

    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    [Header("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f,4f)]
    public float powerMultiplier = 2;
    [Range(7f,14f)]
    public float divisionMultiplier = 7;

    [Space]
    
    [Header("Zombies")]
    public int numberOfZombies;
    public int remainingZombies;
    public float attackCooldown;
    
    
    [Space]
    public float zombieHealthUpgrade;
    public int zombieDamageUpgrade;
    public float zombieSpeedUpgrade;
    [Space]
    public int zombieChaseRange;
    public float waitTime;
    public float startWaitTime;
    [Space]
    public bool zombieChasing;
    public bool zombiePatrolling;
    public bool zombiesDead;
    public bool zombiesSpawned;
    [Space]
    public TMP_Text zombieCounterText;

    [Space]

    public int currentFuel;
    public int minfuelGain;
    public int maxfuelGain;
    public TMP_Text fuelAmount;
    public TMP_Text fuelAmountStore;

    [Space]

    [Header("Store")]

    [Space]

    public int healthCost = 150;
    public int damageCost = 200;
    public int speedCost = 500;
    public int projectileCost = 75;
    public int fuelCost = 85;
    public int medKitCost = 80;

    [Space]

    public int healthUpgradeInterval = 20;
    public float damageUpgradeInterval = 1;
    public float speedUpgradeInterval = 1;
    public int bulletSpeedUpgradeInterval = 5;
    public int FuelUpgradeInterval = 5;
    public int MedkitUpgradeInterval = 10;

    [Space]

    public int maxNoOfHealth;
    public int maxNoOfDamage;
    public int maxNoOfSpeed;
    public int maxNoOfBulletSpeed;
    public int maxNoOfFuel;
    public int maxNoOfMedkit;

    [Space]
    
    public int healthXp;
    public int damageXp;
    public int speadXp;
    public int bulletXp;
    public int FuelXp;
    public int MedKitXp;

    [Space]

    public int noOfHealth;
    public int noOfDamage;
    public int noOfSpeed;
    public int noOfBulletSpeed;
    public int noOfFuel;
    public int noOfMedKit;

    [Space]

    public TMP_Text healthStat;
    public TMP_Text dmgStat;
    public TMP_Text speedStat;
    public TMP_Text projectileSpeedStat;
    public TMP_Text FuelGainStat;
    public TMP_Text MedKitStat;

    [Space]

    public TMP_Text healthCostText;
    public TMP_Text damageCostText;
    public TMP_Text speedCostText;
    public TMP_Text projectileCostText;
    public TMP_Text fuelCostText;
    public TMP_Text MedKitCostText;

    [Space]
    public GameObject maxHealthImage;
    public GameObject maxDamageImage;
    public GameObject maxSpeedImage;
    public GameObject maxBulletImage;
    public GameObject maxFuelImage;
    public GameObject maxMedkitImage;

    [Space]
    public GameObject maxHealthButton;
    public GameObject maxDamageButton;
    public GameObject maxSpeedButton;
    public GameObject maxBulletButton;
    public GameObject maxFuelButton;
    public GameObject maxMedKitButton;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;

        canMove = true;

        storeEnabled = false;
        //level = 1;
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        requiredXp = CalculateRequiredXp();
        levelText.text = "" + level;
    }

    void Update()
    {
        UpdateXpUI();

        if (inDialogue == true || storeEnabled == true || gameIsPaused == true)
        {
            canShoot = false;
        } else {
            canShoot = true;
        }

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            FlatRateExperienceGain(testXP);
        }

        if (currentXp >= requiredXp)
        {
            LevelUp();
        }

        fuelAmount.SetText("Fuel:" + currentFuel);
        fuelAmountStore.SetText("Fuel:" + currentFuel);

        healthStat.SetText("+" + noOfHealth);
        dmgStat.SetText("+" + noOfDamage);
        speedStat.SetText("+" + noOfSpeed);
        projectileSpeedStat.SetText("+" + noOfBulletSpeed);
        FuelGainStat.SetText("+" + noOfFuel);
        MedKitStat.SetText("+" + noOfMedKit);

        if(noOfHealth < maxNoOfHealth)
        {
            healthCostText.SetText("" + healthCost);
        } else {
            healthCostText.SetText("Maxed");
            maxHealthImage.SetActive(true);
            maxHealthButton.SetActive(true);
        }

        if(noOfDamage < maxNoOfDamage)
        {
            damageCostText.SetText("" + damageCost);
        } else {
            damageCostText.SetText("Maxed");
            maxDamageImage.SetActive(true);
            maxDamageButton.SetActive(true);
        }
        if(noOfSpeed < maxNoOfSpeed)
        {
            speedCostText.SetText("" + speedCost);
        } else {
            speedCostText.SetText("Maxed");
            maxSpeedImage.SetActive(true);
            maxSpeedButton.SetActive(true);
        }
        if(noOfBulletSpeed < maxNoOfBulletSpeed)
        {
            projectileCostText.SetText("" + projectileCost);
        } else {
            projectileCostText.SetText("Maxed");
            maxBulletImage.SetActive(true);
            maxBulletButton.SetActive(true);
        }
        if(noOfFuel < maxNoOfFuel)
        {
            fuelCostText.SetText("" + fuelCost);
        } else {
            fuelCostText.SetText("Maxed");
            maxFuelImage.SetActive(true);
            maxFuelButton.SetActive(true);
        }
        if(noOfMedKit < maxNoOfMedkit)
        {
            MedKitCostText.SetText("" + medKitCost);
        } else {
            MedKitCostText.SetText("Maxed");
            maxMedkitImage.SetActive(true);
            maxMedKitButton.SetActive(true);
        }

        zombieCounterText.SetText("Zombies Remaining:" + remainingZombies);

        if (remainingZombies <= 0 && zombiesSpawned == true)
        {
            favourCompleted = true;
        }

        if (inDialogue == true)
        {
            HUD.SetActive(false);
        } else if (inDialogue == false && gameIsPaused == false && storeEnabled == false)
        {
            HUD.SetActive(true);
        }
        
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = frontXpBar.fillAmount;
        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if(delayTimer > 0.25)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXp + "/" + requiredXp;
    }

    public void FlatRateExperienceGain(float xpGained)
    {
        currentXp += xpGained;
        xpGained += 50;
        lerpTimer = 0f;
    }

    public void ScalableExperienceGain(float xpGained, int passedLevel)
    {
        if(passedLevel < level)
        {
            float multplier = 1 + (level - passedLevel) * 0.1f;
            currentXp += xpGained * multplier;
        }
        else
        {
            currentXp += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        frontXpBar.fillAmount = 0;
        backXpBar.fillAmount = 0;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        playerCurrentHealth = playerMaxHealth;
        requiredXp = CalculateRequiredXp();
        /*zombieMaxHealth += zombieHealthUpgrade;
        zombieDamage += zombieDamageUpgrade;
        zombieChaseSpeed += zombieSpeedUpgrade;
        zombiePatrolSpeed += zombieSpeedUpgrade;*/
        levelText.text = "" + level;
        
    }

    public int CalculateRequiredXp()
    {
        int solvedForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solvedForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solvedForRequiredXp / 4;
    }

   
}
