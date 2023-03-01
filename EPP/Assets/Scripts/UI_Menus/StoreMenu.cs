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

    public GameObject healthMax;
    public GameObject damageMax;
    public GameObject speedMax;
    public GameObject bulletMax;
    public GameObject fuelMax;
    public GameObject medMax;

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

    

    void Start ()
    {
        MaxPointSliders();
    }

    void Update()
    {
        _enemy = GameObject.FindWithTag("Zombie");

        //UpdateStatProgressUI();
        healthPoint.value = gameManager.noOfHealth;

        levelNumber.text = "" + gameManager.levelText.text;
        frontXpBar.fillAmount = gameManager.frontXpBar.fillAmount;
        backXpBar.fillAmount = gameManager.backXpBar.fillAmount;


        if (Input.GetKeyDown(KeyCode.Tab) && gameManager.inDialogue == false)
        {
            if(gameManager.storeEnabled)
            {
                StoreOff();
            } else if (gameManager.gameIsPaused == false)
            {
                Store();
            }
        }

        //PointBars();
        PointSliders();
        
        
    }

    public void PointSliders()
    {
        healthPoint.value = gameManager.noOfHealth;
        damagePoint.value = gameManager.noOfDamage;
        speedPoint.value = gameManager.noOfSpeed;
        bulletPoint.value = gameManager.noOfBulletSpeed;
        fuelPoint.value = gameManager.noOfFuel;
        medPoint.value = gameManager.noOfMedKit;

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
    }
    public void MaxPointSliders()
    {
        healthPoint.maxValue = gameManager.maxNoOfHealth;
        damagePoint.maxValue = gameManager.maxNoOfDamage;
        speedPoint.maxValue = gameManager.maxNoOfSpeed;
        bulletPoint.maxValue = gameManager.maxNoOfBulletSpeed;
        fuelPoint.maxValue = gameManager.maxNoOfFuel;
        medPoint.maxValue = gameManager.maxNoOfMedkit;
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
        Time.timeScale = 0f;
        gameManager.storeEnabled = true;
    }

    public void StoreOff()
    {
        HUD.SetActive(true);
        currencyDisplay.SetActive(true);
        remainingZombiesCounter.SetActive(true);
        storeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameManager.storeEnabled = false;
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
            gameManager.playerDamage += gameManager.damageUpgradeInterval;

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
            gameManager.projectileSpeed += gameManager.bulletSpeedUpgradeInterval;

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


}
