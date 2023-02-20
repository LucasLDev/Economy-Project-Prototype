using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreMenu : MonoBehaviour
{

    [Space]

    public GameObject storeMenuUI;
    public GameObject healthBar;
    public GameObject currencyDisplay;

    [Space]

    public GameManager gameManager;
    public Currency currency;

    [Space]

    private GameObject _currency;
    [Space]
    public TMP_Text currencyAmount;

    void Start()
    {

    }


    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(gameManager.storeEnabled)
            {
                StoreOff();
            } else 
            {
                Store();
            }
        }
    }

    public void Store()
    {
        
        healthBar.SetActive(false);
        currencyDisplay.SetActive(false);
        storeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameManager.storeEnabled = true;
    }

    public void StoreOff()
    {
        healthBar.SetActive(true);
        currencyDisplay.SetActive(true);
        storeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameManager.storeEnabled = false;
    }

    public void HealthIncrease()
    {
        if (gameManager.currentFuel >= gameManager.healthCost && gameManager.playerMaxHealth < gameManager.maxUpgradedHealth)
        {
            gameManager.playerMaxHealth++;
            
            gameManager.playerHealthBar.maxValue = gameManager.playerMaxHealth;
            
            gameManager.currentFuel -= gameManager.healthCost;
            
            gameManager.healthCost += gameManager.healthCost * 1/2;
            
            
            Debug.Log("Health Increased");
        }
        
    }

    public void DamageIncrease()
    {
        if (gameManager.currentFuel >= gameManager.damageCost && gameManager.playerDamage < gameManager.maxUpgradedDamage)
        {
            gameManager.playerDamage++;
            
            gameManager.currentFuel -= gameManager.damageCost;
            
            gameManager.damageCost += gameManager.damageCost * 1/2;
            
            Debug.Log("Damage Increased");
        }
        
    }

    public void SpeedIncrease()
    {
        if (gameManager.currentFuel >= gameManager.speedCost && gameManager.playerMoveSpeed < gameManager.maxUpgradedSpeed)
        {
            gameManager.playerMoveSpeed++;
            
            gameManager.currentFuel -= gameManager.speedCost;
            
            gameManager.speedCost += gameManager.speedCost * 1/2;
            
            Debug.Log("Speed Increased");
        }
        
    }

    public void ProjectileSpeedIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fuelCost && gameManager.projectileSpeed < gameManager.maxUpgradedProjectile)
        {
            gameManager.projectileSpeed++;
            
            gameManager.currentFuel -= gameManager.projectileCost;
            
            gameManager.projectileCost += gameManager.projectileCost * 1/2;
            
            Debug.Log("Projectile Speed Increased");
        }
        
    }

    public void CurrencyGainIncrease()
    {
        if (gameManager.currentFuel >= gameManager.fuelCost && currency.currencyGain < gameManager.maxUpgradedFuel)
        {
            gameManager.fuelGain++;
            
            gameManager.currentFuel -= gameManager.fuelCost;
            
            gameManager.fuelCost += gameManager.fuelCost * 1/2;
            
            Debug.Log("Currency Gain Increased");
        }
        
    }


}
