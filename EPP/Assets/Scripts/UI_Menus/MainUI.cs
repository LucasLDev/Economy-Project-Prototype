using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI mainUI;
    [Space]
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text fuelText;
    [Space]
    public TMP_Text trackerTitle;
    public TMP_Text trackerDesc;
    public TMP_Text trackerProgress;

    //public  GameObject storeMenuUI;
    public  GameObject HUD;
    public  GameObject currencyDisplay;
    public  GameObject remainingZombiesCounter;
    public  GameObject equipHandgun;
    public  GameObject equipShotgun;
    public  GameObject buyShotgun;
    public  GameObject buyAR;
    public  GameObject equipAR;
    public GameObject reloadIdicator;
    public Animator animator;

    public Shop shopNPC;

    public bool shotgunBought = false;
    public bool assaultBought = false;
    
    /*public Image[] healthPoints;
    public Image[] damagePoints;
    public Image[] speedPoints;
    public Image[] bulletPoints;
    public Image[] fuelPoints;
    public Image[] medKitPoints;*/
    public Slider healthPoint;
    public Slider damagePoint;
    public Slider speedPoint;
    public Slider bulletPoint;
    public Slider fuelPoint;
    public Slider medPoint;
    public Slider fireRatePoint;
    public Slider ammoPoint;

    public GameObject healthMax;
    public GameObject damageMax;
    public GameObject speedMax;
    public GameObject bulletMax;
    public GameObject fuelMax;
    public GameObject medMax;
    public GameObject fireMax;
    public GameObject ammoMax;


    public  Image mainFrontXpBar;
    public  Image mainBackXpBar;
    public  TextMeshProUGUI levelText;
    public  TextMeshProUGUI xpText;
    [Space]
    public float hChipSpeed = 2f;
    public float hLerpTimer;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Space]

    private GameObject _enemy;

    [Space]
    public TMP_Text currencyAmount;
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelNumber;
    public GameObject maxBar;
    public GameObject shopMaxBar;

    [Header("Text")]
    public TMP_Text fuelAmount;
    public TMP_Text fuelAmountStore;
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
    //public GameObject shotgunBought;

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
    public TMP_Text weaponText;
    public TMP_Text ammoText;
    [Space]
    public GameObject hgIcon;
    public GameObject arIcon;
    public GameObject sgIcon;
    [Space]
    public  GameObject[] Zombies;
    public TMP_Text zombieCounterText;

    /* void Awake()
    {
        if(mainUI == null)
        {
            mainUI = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    } */

    

    void Start ()
    {
        MaxPointSliders();
        maxBar.SetActive(false);
        shopMaxBar.SetActive(false);
        equipHandgun.SetActive(false);
        equipShotgun.SetActive(false);
        equipAR.SetActive(false);

        mainFrontXpBar.fillAmount = GameManager.gameManager.currentXp / GameManager.gameManager.requiredXp;
        mainBackXpBar.fillAmount = GameManager.gameManager.currentXp / GameManager.gameManager.requiredXp;
        levelText.text = "" +  GameManager.gameManager.level;
    }

    void Update()
    {

        _enemy = GameObject.FindWithTag("Zombie");
        

        levelNumber.text = "" + GameManager.gameManager.level;
        frontXpBar.fillAmount = frontXpBar.fillAmount;
        backXpBar.fillAmount = backXpBar.fillAmount;

        TextUpdate();
        MaxCheck();
        StoreCheck();
        UpdateHealthUI();
        PointSliders();
        AmmoIcon();
        CurrentAmmoCounter();
        
        Zombies = GameObject.FindGameObjectsWithTag("Zombie");
        int numberOfZombies = Zombies.Length;
        zombieCounterText.SetText("Zombies Remaining:" + numberOfZombies);

        if (Zombies.Length <= 0 && GameManager.gameManager.zombiesSpawned == true)
        {
            GameManager.gameManager.favourCompleted = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.gameManager.storeEnabled)
            {
                StoreOff();
            } else if (GameManager.gameManager.gameIsPaused == false)
            {
                return;
            }
        }

        //PointBars();
        
        if (GameManager.gameManager.inDialogue == true)
        {
            HUD.SetActive(false);
        } else if (GameManager.gameManager.inDialogue == false && GameManager.gameManager.gameIsPaused == false && GameManager.gameManager.storeEnabled == false)
        {
            HUD.SetActive(true);
        }
    }

    public void StoreCheck()
    {
        //Shotgun
        if (GameManager.gameManager.shotgunStore == true && shotgunBought == false)
        {
            buyShotgun.SetActive(true);
            buyAR.SetActive(false);
            equipAR.SetActive(false);
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(false);
        } else if (GameManager.gameManager.shotgunStore == true && shotgunBought == true && GameManager.gameManager.shotgun == true)
        {
            buyShotgun.SetActive(false);
            buyAR.SetActive(false);
            equipHandgun.SetActive(true);
            equipShotgun.SetActive(false);
            equipAR.SetActive(false);
        } else if (GameManager.gameManager.shotgunStore == true && shotgunBought == true && GameManager.gameManager.shotgun == false)
        {
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(true);
            equipAR.SetActive(false);
            
        }

        //Assault Rifle
        if (GameManager.gameManager.assaultRifleStore == true && assaultBought == false)
        {
            buyShotgun.SetActive(false);
            buyAR.SetActive(true);
            equipAR.SetActive(false);
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(false);
        } else if (GameManager.gameManager.assaultRifleStore == true && assaultBought == true && GameManager.gameManager.AssaultRifle == true)
        {
            buyAR.SetActive(false);
            buyShotgun.SetActive(false);
            equipHandgun.SetActive(true);
            equipShotgun.SetActive(false);
            equipAR.SetActive(false);
        } else if (GameManager.gameManager.assaultRifleStore == true && assaultBought == true && GameManager.gameManager.AssaultRifle == false)
        {
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(false);
            equipAR.SetActive(true);
        }
    }


    public void MaxCheck()
    {
        if(GameManager.gameManager.noOfHealth != GameManager.gameManager.maxNoOfHealth) return;
        if(GameManager.gameManager.noOfDamage != GameManager.gameManager.maxNoOfDamage) return;
        if(GameManager.gameManager.noOfSpeed != GameManager.gameManager.maxNoOfSpeed) return;
        if(GameManager.gameManager.noOfBulletSpeed != GameManager.gameManager.maxNoOfBulletSpeed) return;
        if(GameManager.gameManager.noOfFuel != GameManager.gameManager.maxNoOfFuel) return;
        if(GameManager.gameManager.noOfMedKit != GameManager.gameManager.maxNoOfMedkit) return;
        if(GameManager.gameManager.noOfFireRate != GameManager.gameManager.maxNoOfFireRate) return;
        if(GameManager.gameManager.noOfMaxAmmo != GameManager.gameManager.maxNoOfMaxAmmo) return;

        maxBar.SetActive(true);
        shopMaxBar.SetActive(true);
        GameManager.gameManager.maxStats = true;
    }

    public void PointSliders()
    {
        healthPoint.value = GameManager.gameManager.noOfHealth;
        damagePoint.value = GameManager.gameManager.noOfDamage;
        speedPoint.value = GameManager.gameManager.noOfSpeed;
        bulletPoint.value = GameManager.gameManager.noOfBulletSpeed;
        fuelPoint.value = GameManager.gameManager.noOfFuel;
        medPoint.value = GameManager.gameManager.noOfMedKit;
        fireRatePoint.value = GameManager.gameManager.noOfFireRate;
        ammoPoint.value = GameManager.gameManager.noOfMaxAmmo;

        if (healthPoint.value == healthPoint.maxValue)
        {
            healthMax.SetActive(true);
        }
        if (damagePoint.value == damagePoint.maxValue)
        {
            damageMax.SetActive(true);
        }
        if (speedPoint.value == speedPoint.maxValue)
        {
            speedMax.SetActive(true);
        }
        if (bulletPoint.value == bulletPoint.maxValue)
        {
            bulletMax.SetActive(true);
        }
        if (fuelPoint.value == fuelPoint.maxValue)
        {
            fuelMax.SetActive(true);
        }
        if (medPoint.value == medPoint.maxValue)
        {
            medMax.SetActive(true);
        }
        if (fireRatePoint.value == fireRatePoint.maxValue)
        {
            fireMax.SetActive(true);
        }
        if (ammoPoint.value == ammoPoint.maxValue)
        {
            ammoMax.SetActive(true);
        }
    }
    public void MaxPointSliders()
    {
        healthPoint.maxValue = GameManager.gameManager.maxNoOfHealth;
        damagePoint.maxValue = GameManager.gameManager.maxNoOfDamage;
        speedPoint.maxValue = GameManager.gameManager.maxNoOfSpeed;
        bulletPoint.maxValue = GameManager.gameManager.maxNoOfBulletSpeed;
        fuelPoint.maxValue = GameManager.gameManager.maxNoOfFuel;
        medPoint.maxValue = GameManager.gameManager.maxNoOfMedkit;
        fireRatePoint.maxValue = GameManager.gameManager.maxNoOfFireRate;
        ammoPoint.maxValue = GameManager.gameManager.maxNoOfMaxAmmo;
    }
        

    public void PointBars()
    {
        /*for (int i = 0; i <healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayPoints(GameManager.gameManager.noOfHealth, i);
        }
        for (int i = 0; i <damagePoints.Length; i++)
        {
            damagePoints[i].enabled = !DisplayPoints(GameManager.gameManager.noOfDamage, i);
        }
        for (int i = 0; i <speedPoints.Length; i++)
        {
            speedPoints[i].enabled = !DisplayPoints(GameManager.gameManager.noOfSpeed, i);
        }
        for (int i = 0; i <bulletPoints.Length; i++)
        {
            bulletPoints[i].enabled = !DisplayPoints(GameManager.gameManager.noOfBulletSpeed, i);
        }
        for (int i = 0; i <fuelPoints.Length; i++)
        {
            fuelPoints[i].enabled = !DisplayPoints(GameManager.gameManager.noOfFuel, i);
        }
        for (int i = 0; i <medKitPoints.Length; i++)
        {
            medKitPoints[i].enabled = !DisplayPoints(GameManager.gameManager.noOfMedKit, i);
        }*/
    }

  

    public void Store()
    {
        
        HUD.SetActive(false);
        currencyDisplay.SetActive(false);
        remainingZombiesCounter.SetActive(false);
        animator.SetBool("isOpen", true);
        Time.timeScale = 1f;
        GameManager.gameManager.storeEnabled = true;
    }

    public void StoreOff()
    {
        animator.SetBool("isOpen", false);
        HUD.SetActive(true);
        currencyDisplay.SetActive(true);
        remainingZombiesCounter.SetActive(true);
        Time.timeScale = 1f;
        GameManager.gameManager.shotgunStore = false;
        GameManager.gameManager.assaultRifleStore = false;
        GameManager.gameManager.machinePistolStore = false;
        GameManager.gameManager.subMachineGunStore = false;
        GameManager.gameManager.storeEnabled = false;
    }

    public void BuyShotgun()
    {
        if(GameManager.gameManager.currentFuel >= GameManager.gameManager.ShotgunCost && GameManager.gameManager.shotgun == false)
        {
            GameManager.gameManager.shotgun = true;
            GameManager.gameManager.handgun = false;
            GameManager.gameManager.machinePistol = false;
            GameManager.gameManager.AssaultRifle = false;
            GameManager.gameManager.subMachineGun = false;

            GameManager.gameManager.currentFuel -= GameManager.gameManager.ShotgunCost;
            
            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.shotgunXp);

            shotgunBought = true;

        }
    }

    public void BuyAR()
    {
        if(GameManager.gameManager.currentFuel >= GameManager.gameManager.ShotgunCost && GameManager.gameManager.AssaultRifle == false)
        {
            GameManager.gameManager.shotgun = false;
            GameManager.gameManager.handgun = false;
            GameManager.gameManager.machinePistol = false;
            GameManager.gameManager.AssaultRifle = true;
            GameManager.gameManager.subMachineGun = false;

            GameManager.gameManager.currentFuel -= GameManager.gameManager.assaultCost;
            
            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.assaultXp);

            assaultBought = true;

        }
    }

    public void EquipHandgun()
    {
        GameManager.gameManager.handgun = true;
        GameManager.gameManager.shotgun = false;
        GameManager.gameManager.machinePistol = false;
        GameManager.gameManager.subMachineGun = false;
        GameManager.gameManager.AssaultRifle = false;
    }

    public void EquipShotgun()
    {
        GameManager.gameManager.handgun = false;
        GameManager.gameManager.shotgun = true;
        GameManager.gameManager.machinePistol = false;
        GameManager.gameManager.subMachineGun = false;
        GameManager.gameManager.AssaultRifle = false;
    }

    public void EquipAR()
    {
        GameManager.gameManager.handgun = false;
        GameManager.gameManager.shotgun = false;
        GameManager.gameManager.machinePistol = false;
        GameManager.gameManager.subMachineGun = false;
        GameManager.gameManager.AssaultRifle = true;
    }

    public void HealthIncrease()
    {
        if (GameManager.gameManager.currentFuel >= GameManager.gameManager.healthCost && GameManager.gameManager.noOfHealth < GameManager.gameManager.maxNoOfHealth)
        {
            GameManager.gameManager.playerMaxHealth += GameManager.gameManager.healthUpgradeInterval;

            GameManager.gameManager.noOfHealth++;
            
            GameManager.gameManager.currentFuel -= GameManager.gameManager.healthCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.healthXp);
            
            GameManager.gameManager.healthCost += GameManager.gameManager.healthCost * 1/4;
            
            Debug.Log("Health Increased");
        }
        
    }

    bool DisplayPoints(int _noOfPoints, int pointNumber)
    {
        return (pointNumber >= _noOfPoints);
    }

    public void DamageIncrease()
    {
        if (GameManager.gameManager.currentFuel >= GameManager.gameManager.damageCost && GameManager.gameManager.noOfDamage < GameManager.gameManager.maxNoOfDamage)
        {
            GameManager.gameManager.handgunDamage += GameManager.gameManager.damageUpgradeInterval;
            GameManager.gameManager.subMachineGunDamage += GameManager.gameManager.damageUpgradeInterval;
            GameManager.gameManager.AssaultRifleDamage += GameManager.gameManager.damageUpgradeInterval;
            GameManager.gameManager.machinePistolDamage += GameManager.gameManager.damageUpgradeInterval;
            GameManager.gameManager.shotgunDamage += GameManager.gameManager.damageUpgradeInterval;

            GameManager.gameManager.noOfDamage++;
            
            GameManager.gameManager.currentFuel -= GameManager.gameManager.damageCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.damageXp);
            
            GameManager.gameManager.damageCost += GameManager.gameManager.damageCost * 45/100;
            
            Debug.Log("Damage Increased");
        }
        
    }

    public void SpeedIncrease()
    {
        if (GameManager.gameManager.currentFuel >= GameManager.gameManager.speedCost && GameManager.gameManager.noOfSpeed < GameManager.gameManager.maxNoOfSpeed)
        {
            GameManager.gameManager.playerMoveSpeed += GameManager.gameManager.speedUpgradeInterval;

            GameManager.gameManager.noOfSpeed++;
            
            GameManager.gameManager.currentFuel -= GameManager.gameManager.speedCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.speadXp);
            
            GameManager.gameManager.speedCost += GameManager.gameManager.speedCost * 10/6;
            
            Debug.Log("Speed Increased");
        }
        
    }

    public void ProjectileSpeedIncrease()
    {
        if (GameManager.gameManager.currentFuel >= GameManager.gameManager.fuelCost && GameManager.gameManager.noOfBulletSpeed < GameManager.gameManager.maxNoOfBulletSpeed)
        {
            GameManager.gameManager.handgunBulletSpeed += GameManager.gameManager.handgunBulletSpeedInterval;
            GameManager.gameManager.machineBulletSpeed += GameManager.gameManager.machineBulletSpeedInterval;
            GameManager.gameManager.subBulletSpeed += GameManager.gameManager.subBulletSpeedInterval;
            GameManager.gameManager.assaultBulletSpeed += GameManager.gameManager.assaultBulletSpeedInterval;
            GameManager.gameManager.shotgunBulletSpeed += GameManager.gameManager.shotgunBulletSpeedInterval;

            GameManager.gameManager.noOfBulletSpeed++;
            
            GameManager.gameManager.currentFuel -= GameManager.gameManager.projectileCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.bulletXp);
            
            GameManager.gameManager.projectileCost += GameManager.gameManager.projectileCost * 1/6;
            
            Debug.Log("Projectile Speed Increased");
        }
        
    }

    public void CurrencyGainIncrease()
    {
        if (GameManager.gameManager.currentFuel >= GameManager.gameManager.fuelCost && GameManager.gameManager.noOfFuel < GameManager.gameManager.maxNoOfFuel)
        {
            GameManager.gameManager.minfuelGain += GameManager.gameManager.FuelUpgradeInterval;
            GameManager.gameManager.maxfuelGain += GameManager.gameManager.FuelUpgradeInterval;

            GameManager.gameManager.noOfFuel++;
            
            GameManager.gameManager.currentFuel -= GameManager.gameManager.fuelCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.FuelXp);
            
            GameManager.gameManager.fuelCost += GameManager.gameManager.fuelCost * 1/6;
            
            Debug.Log("Currency Gain Increased");
        }
        
    }

    public void MedKitIncrease()
    {
        if(GameManager.gameManager.currentFuel >= GameManager.gameManager.medKitCost && GameManager.gameManager.noOfMedKit < GameManager.gameManager.maxNoOfMedkit)
        {
            GameManager.gameManager.medkitPotency += GameManager.gameManager.MedkitUpgradeInterval;

            GameManager.gameManager.noOfMedKit++;

            GameManager.gameManager.currentFuel -= GameManager.gameManager.medKitCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.MedKitXp);

            GameManager.gameManager.medKitCost += GameManager.gameManager.medKitCost * 1/4;

            Debug.Log("Med Kit Potency Increased");
        }
    }

    public void FireRateIncrease()
    {
        if (GameManager.gameManager.currentFuel >= GameManager.gameManager.fireRateCost && GameManager.gameManager.noOfFireRate < GameManager.gameManager.maxNoOfFireRate)
        {
            GameManager.gameManager.handgunFireRate -= GameManager.gameManager.handgunFireRateInterval;
            GameManager.gameManager.machinePistolFireRate -= GameManager.gameManager.machineFireRateInterval;
            GameManager.gameManager.subMachineGunFireRate -= GameManager.gameManager.subFireRateInterval;
            GameManager.gameManager.AssaultRifleFireRate -= GameManager.gameManager.assaultFireRateInterval;
            GameManager.gameManager.shotgunFireRate -= GameManager.gameManager.shotgunFireRateInterval;

            GameManager.gameManager.noOfFireRate++;

            GameManager.gameManager.currentFuel -= GameManager.gameManager.fireRateCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.FireRateXp);
            GameManager.gameManager.fireRateCost += GameManager.gameManager.fireRateCost * 1/5;
        }
    }

    public void AmmoIncrease()
    {
        if(GameManager.gameManager.currentFuel >= GameManager.gameManager.maxAmmoCost && GameManager.gameManager.noOfMaxAmmo < GameManager.gameManager.maxNoOfMaxAmmo)
        {
            GameManager.gameManager.handgunMaxAmmo += GameManager.gameManager.handgunAmmointerval;
            GameManager.gameManager.machineMaxAmmo += GameManager.gameManager.machineAmmoInterval;
            GameManager.gameManager.subMaxAmmo += GameManager.gameManager.subAmmoInterval;
            GameManager.gameManager.assaultMaxAmmo += GameManager.gameManager.assaultAmmoInterval;
            GameManager.gameManager.shotgunMaxAmmo += GameManager.gameManager.shotgunAmmoInterval;

            GameManager.gameManager.noOfMaxAmmo++;

            GameManager.gameManager.handgunCurrentAmmo = GameManager.gameManager.handgunMaxAmmo;
            GameManager.gameManager.machineCurrentAmmo = GameManager.gameManager.machineMaxAmmo;
            GameManager.gameManager.subCurrentAmmo = GameManager.gameManager.subMaxAmmo;
            GameManager.gameManager.assaultCurrentAmmo = GameManager.gameManager.assaultMaxAmmo;
            GameManager.gameManager.shotgunCurrentAmmo = GameManager.gameManager.shotgunMaxAmmo;

            GameManager.gameManager.currentFuel -= GameManager.gameManager.maxAmmoCost;

            GameManager.gameManager.FlatRateExperienceGain(GameManager.gameManager.maxAmmoXp);
            GameManager.gameManager.maxAmmoCost += GameManager.gameManager.maxAmmoCost * 45/100;
        }
    }

    public void TextUpdate()
    {
        fuelAmount.SetText("Fuel:" + GameManager.gameManager.currentFuel);
        fuelAmountStore.SetText("Fuel:" + GameManager.gameManager.currentFuel);

        healthStat.SetText("+" + GameManager.gameManager.noOfHealth);
        dmgStat.SetText("+" + GameManager.gameManager.noOfDamage);
        speedStat.SetText("+" + GameManager.gameManager.noOfSpeed);
        projectileSpeedStat.SetText("+" + GameManager.gameManager.noOfBulletSpeed);
        FuelGainStat.SetText("+" + GameManager.gameManager.noOfFuel);
        MedKitStat.SetText("+" + GameManager.gameManager.noOfMedKit);
        FireRateStat.SetText("+" + GameManager.gameManager.noOfFireRate);
        maxAmmoStat.SetText("+" + GameManager.gameManager.noOfMaxAmmo);

        shotgunCostText.SetText("" + GameManager.gameManager.ShotgunCost);
        assaultCostText.SetText("" + GameManager.gameManager.assaultCost);

        if(GameManager.gameManager.noOfHealth < GameManager.gameManager.maxNoOfHealth)
        {
            healthCostText.SetText("" + GameManager.gameManager.healthCost);
        } else {
            healthCostText.SetText("Maxed");
            maxHealthImage.SetActive(true);
            maxHealthButton.SetActive(true);
        }

        if(GameManager.gameManager.noOfDamage < GameManager.gameManager.maxNoOfDamage)
        {
            damageCostText.SetText("" + GameManager.gameManager.damageCost);
        } else {
            damageCostText.SetText("Maxed");
            maxDamageImage.SetActive(true);
            maxDamageButton.SetActive(true);
        }
        if(GameManager.gameManager.noOfSpeed < GameManager.gameManager.maxNoOfSpeed)
        {
            speedCostText.SetText("" + GameManager.gameManager.speedCost);
        } else {
            speedCostText.SetText("Maxed");
            maxSpeedImage.SetActive(true);
            maxSpeedButton.SetActive(true);
        }
        if(GameManager.gameManager.noOfBulletSpeed < GameManager.gameManager.maxNoOfBulletSpeed)
        {
            projectileCostText.SetText("" + GameManager.gameManager.projectileCost);
        } else {
            projectileCostText.SetText("Maxed");
            maxBulletImage.SetActive(true);
            maxBulletButton.SetActive(true);
        }
        if(GameManager.gameManager.noOfFuel < GameManager.gameManager.maxNoOfFuel)
        {
            fuelCostText.SetText("" + GameManager.gameManager.fuelCost);
        } else {
            fuelCostText.SetText("Maxed");
            maxFuelImage.SetActive(true);
            maxFuelButton.SetActive(true);
        }
        if(GameManager.gameManager.noOfMedKit < GameManager.gameManager.maxNoOfMedkit)
        {
            MedKitCostText.SetText("" + GameManager.gameManager.medKitCost);
        } else {
            MedKitCostText.SetText("Maxed");
            maxMedkitImage.SetActive(true);
            maxMedKitButton.SetActive(true);
        }
        if(GameManager.gameManager.noOfFireRate < GameManager.gameManager.maxNoOfFireRate)
        {
            FireRateCostText.SetText("" + GameManager.gameManager.fireRateCost);
        } else {
            FireRateCostText.SetText("Maxed");
            maxFireRateImage.SetActive(true);
            maxFireRateButton.SetActive(true);
        }
        if(GameManager.gameManager.noOfMaxAmmo < GameManager.gameManager.maxNoOfMaxAmmo)
        {
            maxAmmoCostText.SetText("" + GameManager.gameManager.maxAmmoCost);
        } else {
            maxAmmoCostText.SetText("Maxed");
            maxAmmoImage.SetActive(true);
            maxAmmoButton.SetActive(true);
        }
    }

    public void CurrentAmmoCounter()
    {
        if(GameManager.gameManager.handgun == true)
        {
            weaponText.SetText("Handgun");
            ammoText.SetText(GameManager.gameManager.handgunCurrentAmmo + "/" + GameManager.gameManager.handgunMaxAmmo);
        }
        if(GameManager.gameManager.shotgun == true)
        {
            weaponText.SetText("Shotgun");
            ammoText.SetText(GameManager.gameManager.shotgunCurrentAmmo + "/" + GameManager.gameManager.shotgunMaxAmmo);
        }
        if(GameManager.gameManager.AssaultRifle == true)
        {
            weaponText.SetText("Assault Rifle");
            ammoText.SetText(GameManager.gameManager.assaultCurrentAmmo + "/" + GameManager.gameManager.assaultMaxAmmo);
        }
    }


    public void UpdateHealthUI()
    {
        
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = GameManager.gameManager.playerCurrentHealth / GameManager.gameManager.playerMaxHealth;
        
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            hLerpTimer += Time.deltaTime;
            float percentComplete = hLerpTimer / hChipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            hLerpTimer += Time.deltaTime;
            float percentComplete = hLerpTimer / hChipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        
        healthText.text = Mathf.Round(GameManager.gameManager.playerCurrentHealth) + "/" + Mathf.Round(GameManager.gameManager.playerMaxHealth);
    }

    public void AmmoIcon()
    {
        if (GameManager.gameManager.handgun == true)
        {
            hgIcon.SetActive(true);
            arIcon.SetActive(false);
            sgIcon.SetActive(false);
        }
        if (GameManager.gameManager.AssaultRifle == true)
        {
            hgIcon.SetActive(false);
            arIcon.SetActive(true);
            sgIcon.SetActive(false);
        }
        if (GameManager.gameManager.shotgun == true)
        {
            hgIcon.SetActive(false);
            arIcon.SetActive(false);
            sgIcon.SetActive(true);
        }
    }

    public void ReloadIndicator()
    {
        if (GameManager.gameManager.handgun == true && GameManager.gameManager.handgunCurrentAmmo == 0)
        {
            reloadIdicator.SetActive(true);

        } else if (GameManager.gameManager.handgun == true && GameManager.gameManager.handgunCurrentAmmo > 0)
        {
            reloadIdicator.SetActive(false);
        }

        if (GameManager.gameManager.shotgun == true && GameManager.gameManager.shotgunCurrentAmmo == 0)
        {
            reloadIdicator.SetActive(true);

        } else if (GameManager.gameManager.shotgun == true && GameManager.gameManager.shotgunCurrentAmmo > 0)
        {
            reloadIdicator.SetActive(false);
        }

        if (GameManager.gameManager.AssaultRifle == true && GameManager.gameManager.assaultCurrentAmmo == 0)
        {
            reloadIdicator.SetActive(true);

        } else if(GameManager.gameManager.AssaultRifle == true && GameManager.gameManager.assaultCurrentAmmo > 0)
        {
            reloadIdicator.SetActive(false);
        }
    }


}
