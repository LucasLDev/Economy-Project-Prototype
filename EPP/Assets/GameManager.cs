using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [Space]

    public  bool gameIsPaused = false;
    public  bool storeEnabled = false;
    public  bool favourCompleted = false;
    public bool favourActive = false;
    public  bool inDialogue = false;
    public  bool canShoot = true;
    public  bool maxStats = false;
    public  bool isReloading;
    [Space]
    public  bool machinePistolStore;
    public  bool subMachineGunStore;
    public  bool assaultRifleStore;
    public  bool shotgunStore;

    [Space]

    [Header("Player")]
    public  int playerMaxHealth = 5;
    public  float playerCurrentHealth;
    public  float playerMoveSpeed = 4;
    //public float playerDamage = 1;
    public  float projectileSpeed = 20f;
    public  int medkitPotency;
    [Space]
    [Header("Handgun")]
    public  bool handgun;
    public  int handgunAmmo;
    public  float handgunFireRate = 0.45f;
    public  float handgunDamage = 2f;
    public  float handgunBulletSpeed = 15f;
    public  int handgunMaxAmmo;
    public  int handgunCurrentAmmo;
    public  float handgunReloadTime;
    [Space]
    [Header("Machine Pistol")]
    public  bool machinePistol;
    public  float machinePistolFireRate = 0.5f;
    public  float machinePistolDamage = 2f;
    public  float machineBulletSpeed = 20f;
    public  int machineMaxAmmo;
    public  int machineCurrentAmmo;
    public  float machineReloadTime;
    [Space]
    [Header("Sub Machine Gun")]
    public  bool subMachineGun;
    public  float subMachineGunFireRate = 0.5f;
    public  float subMachineGunDamage = 2f;
    public  float subBulletSpeed = 25f;
    public  int subMaxAmmo;
    public  int subCurrentAmmo;
    public  float subReloadTime;
    [Space]
    [Header("Assault Rifle")]
    public  bool AssaultRifle;
    public  float AssaultRifleFireRate = 0.5f;
    public  float AssaultRifleDamage = 2f;
    public  float assaultBulletSpeed = 12.5f;
    public  int assaultMaxAmmo;
    public  int assaultCurrentAmmo;
    public  float assaultReloadTime;
    [Space]
    [Header("Shotgun")]
    public  bool shotgun;
    public  int shotgunAmmo;
    public  float shotgunFireRate = 1.5f;
    public  float shotgunDamage = 4f;
    public  float spread;
    public  float shotgunBulletSpeed = 8.5f;
    public  int shotgunMaxAmmo;
    public  int shotgunCurrentAmmo;
    public  float shotgunReloadTime;
    [Space]
    public  bool inSafeZone;
    public  bool canMove;

    [Header("Level System")]
    public  int level;
    public  float currentXp;
    public  float requiredXp;
    public  float testXP;


    [Header("Multipliers")]
    [Range(1f,300f)]
    public  float additionMultiplier = 300;
    [Range(2f,4f)]
    public  float powerMultiplier = 2;
    [Range(7f,14f)]
    public  float divisionMultiplier = 7;

    [Space]
    
    [Header("Zombies")]
    public  int numberOfZombies;
    public  int remainingZombies;
    public  float attackCooldown;
    
    
    [Space]
    public  float zombieHealthUpgrade;
    public  int zombieDamageUpgrade;
    public  float zombieSpeedUpgrade;
    [Space]
    public  int zombieChaseRange;
    public  float waitTime;
    public  float startWaitTime;
    [Space]
    public  bool zombieChasing;
    public  bool zombiePatrolling;
    public  bool zombiesDead;
    public  bool zombiesSpawned;
    [Space]

    public  int currentFuel;
    public  int minfuelGain;
    public  int maxfuelGain;
    public TMP_Text fuelAmount;
    public TMP_Text fuelAmountStore;

    [Space]

    [Header("Store")]

    [Space]

    public  int healthCost = 150;
    public  int damageCost = 200;
    public  int speedCost = 500;
    public  int projectileCost = 75;
    public  int fuelCost = 85;
    public  int medKitCost = 80;
    public  int fireRateCost = 100;
    public  int maxAmmoCost = 215;
    public  int ShotgunCost = 1500;
    public  int assaultCost = 750;

    [Space]

    public  int healthUpgradeInterval = 20;
    public  float damageUpgradeInterval = 1;
    public  float speedUpgradeInterval = 1;
    public  int FuelUpgradeInterval = 5;
    public  int MedkitUpgradeInterval = 10; 
    //public int bulletSpeedUpgradeInterval = 5;
    [Space]
    public  float handgunBulletSpeedInterval = 0.5f;
    public  float machineBulletSpeedInterval = 0.5f;
    public  float subBulletSpeedInterval = 0.5f;
    public  float assaultBulletSpeedInterval = 0.5f;
    public  float shotgunBulletSpeedInterval = 0.5f;
    [Space]
    
    public  float handgunFireRateInterval = 0.05f;
    public  float machineFireRateInterval = 0.05f;
    public  float subFireRateInterval = 0.05f;
    public  float assaultFireRateInterval = 0.05f;
    public  float shotgunFireRateInterval = 0.05f;
    [Space]
    public  int handgunAmmointerval = 2;
    public  int machineAmmoInterval = 2;
    public  int subAmmoInterval = 3;
    public  int assaultAmmoInterval = 5;
    public  int shotgunAmmoInterval = 5;
    [Space]

    public  int maxNoOfHealth;
    public  int maxNoOfDamage;
    public  int maxNoOfSpeed;
    public  int maxNoOfBulletSpeed;
    public  int maxNoOfFuel;
    public  int maxNoOfMedkit;
    public  int maxNoOfFireRate;
    public  int maxNoOfMaxAmmo;

    [Space]
    
    public  int healthXp;
    public  int damageXp;
    public  int speadXp;
    public  int bulletXp;
    public  int FuelXp;
    public  int MedKitXp;
    public  int FireRateXp;
    public  int maxAmmoXp;
    public  int shotgunXp;
    public  int assaultXp;
    

    [Space]

    public  int noOfHealth;
    public  int noOfDamage;
    public  int noOfSpeed;
    public  int noOfBulletSpeed;
    public  int noOfFuel;
    public  int noOfMedKit;
    public  int noOfFireRate;
    public  int noOfMaxAmmo;

    [Space]
    private MainUI ui;
    [Space]

    public float chipSpeed = 2f;
    [HideInInspector]
    public float delayTimer;
    public float lerpTimer;

    [Space]
    public  GameObject[] objectsWithTag;
    
    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
        LevelUp();
        playerCurrentHealth = playerMaxHealth;
        handgun = true;
        machinePistol = false;
        subMachineGun = false;
        AssaultRifle = false;
        shotgun = false;

        canMove = true;

        storeEnabled = false;
        //level = 1;
    }

    void Update()
    {
        ShootCheck();
        
        LevelUp();
        UpdateXpUI();
        /* UpdateStoreXpUI(); */
        

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            FlatRateExperienceGain(testXP);
        }

    }

    public  void ShootCheck()
    {
        if (inDialogue == true || storeEnabled == true || gameIsPaused == true)
        {
            canShoot = false;
        } else {
            canShoot = true;
        }
    }



    public  void FlatRateExperienceGain(float xpGained)
    {
        currentXp += xpGained;
        xpGained += 50;
        lerpTimer = 0f;
    }

    /* public  void ScalableExperienceGain(float xpGained, int passedLevel)
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
    } */

    public  void LevelUp()
    {
        if (currentXp >= requiredXp)
        {
            level++;
            ui.mainFrontXpBar.fillAmount = 0;
            ui.mainBackXpBar.fillAmount = 0;
            currentXp = Mathf.RoundToInt(currentXp - requiredXp);
            playerCurrentHealth = playerMaxHealth;
            requiredXp = CalculateRequiredXp();
            //ui.levelText.text = "" + level;
        }
        
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = ui.mainFrontXpBar.fillAmount;
        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            ui.mainBackXpBar.fillAmount = xpFraction;
            ui.backXpBar.fillAmount = xpFraction;
            if(delayTimer > 0.25)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                ui.mainFrontXpBar.fillAmount = Mathf.Lerp(FXP, ui.mainBackXpBar.fillAmount, percentComplete);
                ui.frontXpBar.fillAmount = Mathf.Lerp(FXP, ui.backXpBar.fillAmount, percentComplete);
            }
        }
        if(maxStats != true)
        {
            ui.xpText.text = currentXp + "/" + requiredXp;
            ui.levelText.text = "" + level;
        } else {
            ui.xpText.text = "Max";
            ui.levelText.text = "" + level;
        }
        
    }

    /* public void UpdateStoreXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = ui.frontXpBar.fillAmount;
        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            ui.backXpBar.fillAmount = xpFraction;
            if(delayTimer > 0.25)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                percentComplete = percentComplete * percentComplete;
                ui.frontXpBar.fillAmount = Mathf.Lerp(FXP, ui.backXpBar.fillAmount, percentComplete);
            }
        }
    } */

    public  int CalculateRequiredXp()
    {
        int solvedForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solvedForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solvedForRequiredXp / 4;
    }

    

   
}
