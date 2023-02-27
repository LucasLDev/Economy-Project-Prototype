using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{

    [Space]

    public GameObject storeMenuUI;
    public GameObject healthBar;
    public GameObject currencyDisplay;
    public GameObject remainingZombiesCounter;
    public LevelSystem level;

    [Space]

    public GameManager gameManager;

    [Space]
    public TMP_Text currencyAmount;
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelNumber;


    void Update()
    {
        levelNumber.text = "" + level.levelText.text;
        frontXpBar.fillAmount = level.frontXpBar.fillAmount;
        backXpBar.fillAmount = level.backXpBar.fillAmount;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(gameManager.storeEnabled)
            {
                StoreOff();
            } else if (gameManager.gameIsPaused == false)
            {
                Store();
            }
        }
    }

    public void Store()
    {
        
        healthBar.SetActive(false);
        currencyDisplay.SetActive(false);
        remainingZombiesCounter.SetActive(false);
        storeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameManager.storeEnabled = true;
    }

    public void StoreOff()
    {
        healthBar.SetActive(true);
        currencyDisplay.SetActive(true);
        remainingZombiesCounter.SetActive(true);
        storeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameManager.storeEnabled = false;
    }

    public void HealthIncrease()
    {
        if (gameManager.currentFuel >= gameManager.healthCost && gameManager.playerMaxHealth < gameManager.maxUpgradedHealth)
        {
            gameManager.playerMaxHealth += gameManager.healthUpgradeInterval;

            gameManager.noOfHealth++;
            
            gameManager.currentFuel -= gameManager.healthCost;

            level.FlatRateExperienceGain(gameManager.healthXp);
            
            gameManager.healthCost += gameManager.healthCost * 1/2;
            
            Debug.Log("Health Increased");
        }
        
    }

    public void DamageIncrease()
    {
        if (gameManager.currentFuel >= gameManager.damageCost && gameManager.playerDamage < gameManager.maxUpgradedDamage)
        {
            gameManager.playerDamage += gameManager.damageUpgradeInterval;

            gameManager.noOfDamage++;
            
            gameManager.currentFuel -= gameManager.damageCost;

            level.FlatRateExperienceGain(gameManager.damageXp);
            
            gameManager.damageCost += gameManager.damageCost * 1/2;
            
            Debug.Log("Damage Increased");
        }
        
    }

    public void SpeedIncrease()
    {
        if (gameManager.currentFuel >= gameManager.speedCost && gameManager.playerMoveSpeed < gameManager.maxUpgradedSpeed)
        {
            gameManager.playerMoveSpeed += gameManager.speedUpgradeInterval;

            gameManager.noOfSpeed++;
            
            gameManager.currentFuel -= gameManager.speedCost;

            level.FlatRateExperienceGain(gameManager.speadXp);
            
            gameManager.speedCost += gameManager.speedCost * 1/2;
            
            Debug.Log("Speed Increased");
        }
        
    }

    public void ProjectileSpeedIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fuelCost && gameManager.projectileSpeed < gameManager.maxUpgradedProjectile)
        {
            gameManager.projectileSpeed += gameManager.bulletSpeedUpgradeInterval;

            gameManager.noOfBulletSpeed++;
            
            gameManager.currentFuel -= gameManager.projectileCost;

            level.FlatRateExperienceGain(gameManager.bulletXp);
            
            gameManager.projectileCost += gameManager.projectileCost * 1/2;
            
            Debug.Log("Projectile Speed Increased");
        }
        
    }

    public void CurrencyGainIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fuelCost && gameManager.minfuelGain < gameManager.maxUpgradedFuel)
        {
            gameManager.minfuelGain += gameManager.FuelUpgradeInterval;
            gameManager.maxfuelGain += gameManager.FuelUpgradeInterval;

            gameManager.noOfFuel++;
            
            gameManager.currentFuel -= gameManager.fuelCost;

            level.FlatRateExperienceGain(gameManager.FuelXp);
            
            gameManager.fuelCost += gameManager.fuelCost * 1/2;
            
            Debug.Log("Currency Gain Increased");
        }
        
    }


}
