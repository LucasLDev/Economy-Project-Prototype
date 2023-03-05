using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{

    [Space]

    public GameObject storeMenuUI;
    public GameObject HUD;
    public GameObject currencyDisplay;
    public GameObject remainingZombiesCounter;
    public GameObject equipHandgun;
    public GameObject equipShotgun;
    public GameObject buyShotgun;
    public GameObject buyAR;
    public GameObject equipAR;

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

    public GameObject healthMax;
    public GameObject damageMax;
    public GameObject speedMax;
    public GameObject bulletMax;
    public GameObject fuelMax;
    public GameObject medMax;
    public GameObject fireMax;

    public float chipSpeed = 2f;
    private float delayTimer;
    private float lerpTimer;

    [Space]

    public GameManager gameManager;
    private GameObject _enemy;

    [Space]
    public TMP_Text currencyAmount;
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelNumber;
    public GameObject maxBar;
    public GameObject shopMaxBar;

    

    void Start ()
    {
        MaxPointSliders();
        maxBar.SetActive(false);
        shopMaxBar.SetActive(false);
        equipHandgun.SetActive(false);
        equipShotgun.SetActive(false);
        equipAR.SetActive(false);
    }

    void Update()
    {

        _enemy = GameObject.FindWithTag("Zombie");

        //UpdateStatProgressUI();
        healthPoint.value = gameManager.noOfHealth;

        levelNumber.text = "" + gameManager.levelText.text;
        frontXpBar.fillAmount = gameManager.frontXpBar.fillAmount;
        backXpBar.fillAmount = gameManager.backXpBar.fillAmount;

        MaxCheck();
        StoreCheck();
        


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameManager.storeEnabled)
            {
                StoreOff();
            } else if (gameManager.gameIsPaused == false)
            {
                return;
            }
        }

        //PointBars();
        PointSliders();
        
        
    }

    public void StoreCheck()
    {
        //Shotgun
        if (gameManager.shotgunStore == true && shotgunBought == false)
        {
            buyShotgun.SetActive(true);
            buyAR.SetActive(false);
            equipAR.SetActive(false);
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(false);
        } else if (gameManager.shotgunStore == true && shotgunBought == true && gameManager.shotgun == true)
        {
            buyShotgun.SetActive(false);
            buyAR.SetActive(false);
            equipHandgun.SetActive(true);
            equipShotgun.SetActive(false);
            equipAR.SetActive(false);
        } else if (gameManager.shotgunStore == true && shotgunBought == true && gameManager.shotgun == false)
        {
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(true);
            equipAR.SetActive(false);
            
        }

        //Assault Rifle
        if (gameManager.assaultRifleStore == true && assaultBought == false)
        {
            buyShotgun.SetActive(false);
            buyAR.SetActive(true);
            equipAR.SetActive(false);
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(false);
        } else if (gameManager.assaultRifleStore == true && assaultBought == true && gameManager.AssaultRifle == true)
        {
            buyAR.SetActive(false);
            buyShotgun.SetActive(false);
            equipHandgun.SetActive(true);
            equipShotgun.SetActive(false);
            equipAR.SetActive(false);
        } else if (gameManager.assaultRifleStore == true && assaultBought == true && gameManager.AssaultRifle == false)
        {
            equipHandgun.SetActive(false);
            equipShotgun.SetActive(false);
            equipAR.SetActive(true);
        }
    }


    public void MaxCheck()
    {
        if(gameManager.noOfHealth != gameManager.maxNoOfHealth) return;
        if(gameManager.noOfDamage != gameManager.maxNoOfDamage) return;
        if(gameManager.noOfSpeed != gameManager.maxNoOfSpeed) return;
        if(gameManager.noOfBulletSpeed != gameManager.maxNoOfBulletSpeed) return;
        if(gameManager.noOfFuel != gameManager.maxNoOfFuel) return;
        if(gameManager.noOfMedKit != gameManager.maxNoOfMedkit) return;
        if(gameManager.noOfFireRate != gameManager.maxNoOfFireRate) return;

        maxBar.SetActive(true);
        shopMaxBar.SetActive(true);
        gameManager.maxStats = true;
    }

    public void PointSliders()
    {
        healthPoint.value = gameManager.noOfHealth;
        damagePoint.value = gameManager.noOfDamage;
        speedPoint.value = gameManager.noOfSpeed;
        bulletPoint.value = gameManager.noOfBulletSpeed;
        fuelPoint.value = gameManager.noOfFuel;
        medPoint.value = gameManager.noOfMedKit;
        fireRatePoint.value = gameManager.noOfFireRate;

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
    }
    public void MaxPointSliders()
    {
        healthPoint.maxValue = gameManager.maxNoOfHealth;
        damagePoint.maxValue = gameManager.maxNoOfDamage;
        speedPoint.maxValue = gameManager.maxNoOfSpeed;
        bulletPoint.maxValue = gameManager.maxNoOfBulletSpeed;
        fuelPoint.maxValue = gameManager.maxNoOfFuel;
        medPoint.maxValue = gameManager.maxNoOfMedkit;
        fireRatePoint.maxValue = gameManager.maxNoOfFireRate;
    }
        

    public void PointBars()
    {
        /*for (int i = 0; i <healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayPoints(gameManager.noOfHealth, i);
        }
        for (int i = 0; i <damagePoints.Length; i++)
        {
            damagePoints[i].enabled = !DisplayPoints(gameManager.noOfDamage, i);
        }
        for (int i = 0; i <speedPoints.Length; i++)
        {
            speedPoints[i].enabled = !DisplayPoints(gameManager.noOfSpeed, i);
        }
        for (int i = 0; i <bulletPoints.Length; i++)
        {
            bulletPoints[i].enabled = !DisplayPoints(gameManager.noOfBulletSpeed, i);
        }
        for (int i = 0; i <fuelPoints.Length; i++)
        {
            fuelPoints[i].enabled = !DisplayPoints(gameManager.noOfFuel, i);
        }
        for (int i = 0; i <medKitPoints.Length; i++)
        {
            medKitPoints[i].enabled = !DisplayPoints(gameManager.noOfMedKit, i);
        }*/
    }

  

    public void Store()
    {
        
        HUD.SetActive(false);
        currencyDisplay.SetActive(false);
        remainingZombiesCounter.SetActive(false);
        storeMenuUI.SetActive(true);
        Time.timeScale = 1f;
        gameManager.storeEnabled = true;
    }

    public void StoreOff()
    {
        HUD.SetActive(true);
        currencyDisplay.SetActive(true);
        remainingZombiesCounter.SetActive(true);
        storeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameManager.shotgunStore = false;
        gameManager.assaultRifleStore = false;
        gameManager.machinePistolStore = false;
        gameManager.subMachineGunStore = false;
        gameManager.storeEnabled = false;
    }

    public void BuyShotgun()
    {
        if(gameManager.currentFuel >= gameManager.ShotgunCost && gameManager.shotgun == false)
        {
            gameManager.shotgun = true;
            gameManager.handgun = false;
            gameManager.machinePistol = false;
            gameManager.AssaultRifle = false;
            gameManager.subMachineGun = false;

            gameManager.currentFuel -= gameManager.ShotgunCost;
            
            gameManager.FlatRateExperienceGain(gameManager.shotgunXp);

            shotgunBought = true;

        }
    }

    public void BuyAR()
    {
        if(gameManager.currentFuel >= gameManager.ShotgunCost && gameManager.shotgun == false)
        {
            gameManager.shotgun = false;
            gameManager.handgun = false;
            gameManager.machinePistol = false;
            gameManager.AssaultRifle = true;
            gameManager.subMachineGun = false;

            gameManager.currentFuel -= gameManager.assaultCost;
            
            gameManager.FlatRateExperienceGain(gameManager.assaultXp);

            assaultBought = true;

        }
    }

    public void EquipHandgun()
    {
        gameManager.handgun = true;
        gameManager.shotgun = false;
        gameManager.machinePistol = false;
        gameManager.subMachineGun = false;
        gameManager.AssaultRifle = false;
    }

    public void EquipShotgun()
    {
        gameManager.handgun = false;
        gameManager.shotgun = true;
        gameManager.machinePistol = false;
        gameManager.subMachineGun = false;
        gameManager.AssaultRifle = false;
    }

    public void EquipAR()
    {
        gameManager.handgun = false;
        gameManager.shotgun = false;
        gameManager.machinePistol = false;
        gameManager.subMachineGun = false;
        gameManager.AssaultRifle = true;
    }

    public void HealthIncrease()
    {
        if (gameManager.currentFuel >= gameManager.healthCost && gameManager.noOfHealth < gameManager.maxNoOfHealth)
        {
            gameManager.playerMaxHealth += gameManager.healthUpgradeInterval;

            gameManager.noOfHealth++;
            
            gameManager.currentFuel -= gameManager.healthCost;

            gameManager.FlatRateExperienceGain(gameManager.healthXp);
            
            gameManager.healthCost += gameManager.healthCost * 1/4;
            
            Debug.Log("Health Increased");
        }
        
    }

    bool DisplayPoints(int _noOfPoints, int pointNumber)
    {
        return (pointNumber >= _noOfPoints);
    }

    public void DamageIncrease()
    {
        if (gameManager.currentFuel >= gameManager.damageCost && gameManager.noOfDamage < gameManager.maxNoOfDamage)
        {
            gameManager.handgunDamage += gameManager.damageUpgradeInterval;
            gameManager.subMachineGunDamage += gameManager.damageUpgradeInterval;
            gameManager.AssaultRifleDamage += gameManager.damageUpgradeInterval;
            gameManager.machinePistolDamage += gameManager.damageUpgradeInterval;
            gameManager.shotgunDamage += gameManager.damageUpgradeInterval;

            gameManager.noOfDamage++;
            
            gameManager.currentFuel -= gameManager.damageCost;

            gameManager.FlatRateExperienceGain(gameManager.damageXp);
            
            gameManager.damageCost += gameManager.damageCost * 45/100;
            
            Debug.Log("Damage Increased");
        }
        
    }

    public void SpeedIncrease()
    {
        if (gameManager.currentFuel >= gameManager.speedCost && gameManager.noOfSpeed < gameManager.maxNoOfSpeed)
        {
            gameManager.playerMoveSpeed += gameManager.speedUpgradeInterval;

            gameManager.noOfSpeed++;
            
            gameManager.currentFuel -= gameManager.speedCost;

            gameManager.FlatRateExperienceGain(gameManager.speadXp);
            
            gameManager.speedCost += gameManager.speedCost * 10/6;
            
            Debug.Log("Speed Increased");
        }
        
    }

    public void ProjectileSpeedIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fuelCost && gameManager.noOfBulletSpeed < gameManager.maxNoOfBulletSpeed)
        {
            gameManager.handgunBulletSpeed += gameManager.handgunBulletSpeedInterval;
            gameManager.machineBulletSpeed += gameManager.machineBulletSpeedInterval;
            gameManager.subBulletSpeed += gameManager.subBulletSpeedInterval;
            gameManager.assaultBulletSpeed += gameManager.assaultBulletSpeedInterval;
            gameManager.shotgunBulletSpeed += gameManager.shotgunBulletSpeedInterval;

            gameManager.noOfBulletSpeed++;
            
            gameManager.currentFuel -= gameManager.projectileCost;

            gameManager.FlatRateExperienceGain(gameManager.bulletXp);
            
            gameManager.projectileCost += gameManager.projectileCost * 1/6;
            
            Debug.Log("Projectile Speed Increased");
        }
        
    }

    public void CurrencyGainIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fuelCost && gameManager.noOfFuel < gameManager.maxNoOfFuel)
        {
            gameManager.minfuelGain += gameManager.FuelUpgradeInterval;
            gameManager.maxfuelGain += gameManager.FuelUpgradeInterval;

            gameManager.noOfFuel++;
            
            gameManager.currentFuel -= gameManager.fuelCost;

            gameManager.FlatRateExperienceGain(gameManager.FuelXp);
            
            gameManager.fuelCost += gameManager.fuelCost * 1/6;
            
            Debug.Log("Currency Gain Increased");
        }
        
    }

    public void MedKitIncrease()
    {
        if(gameManager.currentFuel >= gameManager.medKitCost && gameManager.noOfMedKit < gameManager.maxNoOfMedkit)
        {
            gameManager.medkitPotency += gameManager.MedkitUpgradeInterval;

            gameManager.noOfMedKit++;

            gameManager.currentFuel -= gameManager.medKitCost;

            gameManager.FlatRateExperienceGain(gameManager.MedKitXp);

            gameManager.medKitCost += gameManager.medKitCost * 1/4;

            Debug.Log("Med Kit Potency Increased");
        }
    }

    public void FireRateIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fireRateCost && gameManager.noOfFireRate < gameManager.maxNoOfFireRate)
        {
            gameManager.handgunFireRate -= gameManager.handgunFireRateInterval;
            gameManager.machinePistolFireRate -= gameManager.machineFireRateInterval;
            gameManager.subMachineGunFireRate -= gameManager.subFireRateInterval;
            gameManager.AssaultRifleFireRate -= gameManager.assaultFireRateInterval;
            gameManager.shotgunFireRate -= gameManager.shotgunFireRateInterval;

            gameManager.noOfFireRate++;

            gameManager.currentFuel -= gameManager.fireRateCost;

            gameManager.FlatRateExperienceGain(gameManager.FireRateXp);
            gameManager.fireRateCost += gameManager.fireRateCost * 1/5;
        }
    }


}
