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
    public bool maxStats = false;
    public bool isReloading;
    [Space]
    public bool machinePistolStore;
    public bool subMachineGunStore;
    public bool assaultRifleStore;
    public bool shotgunStore;
    [Space]

    public GameObject HUD;
    public StoreMenu store;

    [Space]

    [Header("Player")]
    public int playerMaxHealth = 5;
    public float playerCurrentHealth;
    public float playerMoveSpeed = 4;
    //public float playerDamage = 1;
    public float projectileSpeed = 20f;
    public int medkitPotency;
    [Space]
    [Header("Handgun")]
    public bool handgun;
    int handgunAmmo;
    public float handgunFireRate = 0.45f;
    public float handgunDamage = 2f;
    public float handgunBulletSpeed = 15f;
    public int handgunMaxAmmo;
    public int handgunCurrentAmmo;
    public float handgunReloadTime;
    [Space]
    [Header("Machine Pistol")]
    public bool machinePistol;
    public float machinePistolFireRate = 0.5f;
    public float machinePistolDamage = 2f;
    public float machineBulletSpeed = 20f;
    public int machineMaxAmmo;
    public int machineCurrentAmmo;
    public float machineReloadTime;
    [Space]
    [Header("Sub Machine Gun")]
    public bool subMachineGun;
    public float subMachineGunFireRate = 0.5f;
    public float subMachineGunDamage = 2f;
    public float subBulletSpeed = 25f;
    public int subMaxAmmo;
    public int subCurrentAmmo;
    public float subReloadTime;
    [Space]
    [Header("Assault Rifle")]
    public bool AssaultRifle;
    public float AssaultRifleFireRate = 0.5f;
    public float AssaultRifleDamage = 2f;
    public float assaultBulletSpeed = 12.5f;
    public int assaultMaxAmmo;
    public int assaultCurrentAmmo;
    public float assaultReloadTime;
    [Space]
    [Header("Shotgun")]
    public bool shotgun;
    public int shotgunAmmo;
    public float shotgunFireRate = 1.5f;
    public float shotgunDamage = 4f;
    public float spread;
    public float shotgunBulletSpeed = 8.5f;
    public int shotgunMaxAmmo;
    public int shotgunCurrentAmmo;
    public float shotgunReloadTime;
    [Space]
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
    public int fireRateCost = 100;
    public int maxAmmoCost = 215;
    public int ShotgunCost = 1500;
    public int assaultCost = 750;

    [Space]

    public int healthUpgradeInterval = 20;
    public float damageUpgradeInterval = 1;
    public float speedUpgradeInterval = 1;
    public int FuelUpgradeInterval = 5;
    public int MedkitUpgradeInterval = 10; 
    //public int bulletSpeedUpgradeInterval = 5;
    [Space]
    public float handgunBulletSpeedInterval = 0.5f;
    public float machineBulletSpeedInterval = 0.5f;
    public float subBulletSpeedInterval = 0.5f;
    public float assaultBulletSpeedInterval = 0.5f;
    public float shotgunBulletSpeedInterval = 0.5f;
    [Space]
    
    public float handgunFireRateInterval = 0.05f;
    public float machineFireRateInterval = 0.05f;
    public float subFireRateInterval = 0.05f;
    public float assaultFireRateInterval = 0.05f;
    public float shotgunFireRateInterval = 0.05f;
    [Space]
    public int handgunAmmointerval = 2;
    public int machineAmmoInterval = 2;
    public int subAmmoInterval = 3;
    public int assaultAmmoInterval = 5;
    public int shotgunAmmoInterval = 5;
    [Space]

    public int maxNoOfHealth;
    public int maxNoOfDamage;
    public int maxNoOfSpeed;
    public int maxNoOfBulletSpeed;
    public int maxNoOfFuel;
    public int maxNoOfMedkit;
    public int maxNoOfFireRate;
    public int maxNoOfMaxAmmo;

    [Space]
    
    public int healthXp;
    public int damageXp;
    public int speadXp;
    public int bulletXp;
    public int FuelXp;
    public int MedKitXp;
    public int FireRateXp;
    public int maxAmmoXp;
    public int shotgunXp;
    public int assaultXp;
    

    [Space]

    public int noOfHealth;
    public int noOfDamage;
    public int noOfSpeed;
    public int noOfBulletSpeed;
    public int noOfFuel;
    public int noOfMedKit;
    public int noOfFireRate;
    public int noOfMaxAmmo;

    [Space]

    public TMP_Text healthStat;
    public TMP_Text dmgStat;
    public TMP_Text speedStat;
    public TMP_Text projectileSpeedStat;
    public TMP_Text FuelGainStat;
    public TMP_Text MedKitStat;
    public TMP_Text FireRateStat;
    public TMP_Text maxAmmoStat;

    [Space]

    public TMP_Text healthCostText;
    public TMP_Text damageCostText;
    public TMP_Text speedCostText;
    public TMP_Text projectileCostText;
    public TMP_Text fuelCostText;
    public TMP_Text MedKitCostText;
    public TMP_Text FireRateCostText;
    public TMP_Text maxAmmoCostText;
    public TMP_Text shotgunCostText;
    public TMP_Text assaultCostText;

    [Space]
    public GameObject maxHealthImage;
    public GameObject maxDamageImage;
    public GameObject maxSpeedImage;
    public GameObject maxBulletImage;
    public GameObject maxFuelImage;
    public GameObject maxMedkitImage;
    public GameObject maxFireRateImage;
    public GameObject maxAmmoImage;
    public GameObject shotgunBought;

    [Space]
    public GameObject maxHealthButton;
    public GameObject maxDamageButton;
    public GameObject maxSpeedButton;
    public GameObject maxBulletButton;
    public GameObject maxFuelButton;
    public GameObject maxMedKitButton;
    public GameObject maxFireRateButton;
    public GameObject maxAmmoButton;
    public GameObject shotgunBoughtButton;

    [Space]
    public GameObject[] objectsWithTag;
    [Space]
    public TMP_Text weaponText;
    public TMP_Text ammoText;
    

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        handgun = true;
        machinePistol = false;
        subMachineGun = false;
        AssaultRifle = false;
        shotgun = false;

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

        ShootCheck();
        
        LevelUp();
        
        TextUpdate();

        CurrentAmmoCounter();

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            FlatRateExperienceGain(testXP);
        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("Zombie");
        int numberOfObjectsWithTag = objectsWithTag.Length;
        zombieCounterText.SetText("Zombies Remaining:" + numberOfObjectsWithTag);

        if (objectsWithTag.Length <= 0 && zombiesSpawned == true)
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

    public void ShootCheck()
    {
        if (inDialogue == true || storeEnabled == true || gameIsPaused == true)
        {
            canShoot = false;
        } else {
            canShoot = true;
        }
    }

    public void TextUpdate()
    {
        fuelAmount.SetText("Fuel:" + currentFuel);
        fuelAmountStore.SetText("Fuel:" + currentFuel);

        healthStat.SetText("+" + noOfHealth);
        dmgStat.SetText("+" + noOfDamage);
        speedStat.SetText("+" + noOfSpeed);
        projectileSpeedStat.SetText("+" + noOfBulletSpeed);
        FuelGainStat.SetText("+" + noOfFuel);
        MedKitStat.SetText("+" + noOfMedKit);
        FireRateStat.SetText("+" + noOfFireRate);
        maxAmmoStat.SetText("+" + noOfMaxAmmo);

        shotgunCostText.SetText("" + ShotgunCost);
        assaultCostText.SetText("" + assaultCost);

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
        if(noOfFireRate < maxNoOfFireRate)
        {
            FireRateCostText.SetText("" + fireRateCost);
        } else {
            FireRateCostText.SetText("Maxed");
            maxFireRateImage.SetActive(true);
            maxFireRateButton.SetActive(true);
        }
        if(noOfMaxAmmo < maxNoOfMaxAmmo)
        {
            maxAmmoCostText.SetText("" + maxAmmoCost);
        } else {
            maxAmmoCostText.SetText("Maxed");
            maxAmmoImage.SetActive(true);
            maxAmmoButton.SetActive(true);
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
        if(maxStats != true)
        {
            xpText.text = currentXp + "/" + requiredXp;
        } else {
            xpText.text = "Max";
        }
        
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
        if (currentXp >= requiredXp)
        {
            level++;
        frontXpBar.fillAmount = 0;
        backXpBar.fillAmount = 0;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        playerCurrentHealth = playerMaxHealth;
        requiredXp = CalculateRequiredXp();
        levelText.text = "" + level;
        }
        
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

    public void CurrentAmmoCounter()
    {
        if(handgun == true)
        {
            weaponText.SetText("Handgun");
            ammoText.SetText(handgunCurrentAmmo + "/" + handgunMaxAmmo);
        }
        if(shotgun == true)
        {
            weaponText.SetText("Shotgun");
            ammoText.SetText(shotgunCurrentAmmo + "/" + shotgunMaxAmmo);
        }
        if(AssaultRifle == true)
        {
            weaponText.SetText("Assault Rifle");
            ammoText.SetText(assaultCurrentAmmo + "/" + assaultMaxAmmo);
        }
    }

   
}
