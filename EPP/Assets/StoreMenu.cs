using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreMenu : MonoBehaviour
{
    public static bool storeEnabled = false;

    [Space]

    public int healthCost = 50;
    public int damageCost = 100;
    public int speedCost = 150;
    public int projectileCost = 25;
    public int currencyCost = 75;

    [Space]

    public TMP_Text healthCostText;
    public TMP_Text damageCostText;
    public TMP_Text speedCostText;
    public TMP_Text projectileCostText;
    public TMP_Text currencyCostText;

    [Space]

    public GameObject storeMenuUI;
    public GameObject healthBar;
    public GameObject currencyDisplay;

    [Space]

    public Health health;
    public PlayerMovement movement;
    public Shooting shooting;
    public Currency currency;

    [Space]

    private GameObject _currency;
    [Space]
    public TMP_Text currencyAmount;

    void Start()
    {
        _currency = GameObject.FindWithTag("GameManager");
        currency = _currency.GetComponent<Currency>();

        health.maxHealth = PlayerPrefs.GetInt("healthValue", health.maxHealth);
        health.playerDamage = PlayerPrefs.GetFloat("damageValue", health.playerDamage);
        movement.moveSpeed = PlayerPrefs.GetInt("speedValue", movement.moveSpeed);
        shooting.bulletForce = PlayerPrefs.GetFloat("projectileValue", shooting.bulletForce);
        currency.currencyGain = PlayerPrefs.GetInt("currencyGainValue", currency.currencyGain);

    }


    void Update()
    {
        healthCostText.SetText("" + healthCost);
        damageCostText.SetText("" + damageCost);
        speedCostText.SetText("" + speedCost);
        projectileCostText.SetText("" + projectileCost);
        currencyCostText.SetText("" + currencyCost);
        currencyAmount.SetText("" + currency.count);
        
        PlayerPrefs.SetInt("amount", currency.count);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(storeEnabled)
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
        storeEnabled = true;
    }

    public void StoreOff()
    {
        healthBar.SetActive(true);
        currencyDisplay.SetActive(true);
        storeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        storeEnabled = false;
    }

    public void HealthIncrease()
    {
        if (currency.count >= healthCost)
        {
            health.maxHealth++;
            PlayerPrefs.SetInt("healthValue", health.maxHealth);
            health.playerHealth.maxValue = health.maxHealth;
            //health.currentHealth = health.maxHealth;
            currency.count -= healthCost;
            healthCost += healthCost * 1/2;
            
            Debug.Log("Health Increased");
        }
        
    }

    public void DamageIncrease()
    {
        if (currency.count >= damageCost)
        {
            health.playerDamage++;
            PlayerPrefs.SetFloat("damageValue", health.playerDamage);
            currency.count -= damageCost;
            damageCost += damageCost * 1/2;
            Debug.Log("Damage Increased");
        }
        
    }

    public void SpeedIncrease()
    {
        if (currency.count >= speedCost)
        {
            movement.moveSpeed++;
            currency.count -= speedCost;
            speedCost += speedCost * 1/2;
            PlayerPrefs.SetInt("speedValue", movement.moveSpeed);
            Debug.Log("Speed Increased");
        }
        
    }

    public void ProjectileSpeedIncrease()
    {
        if (currency.count >= projectileCost)
        {
            shooting.bulletForce++;
            currency.count -= projectileCost;
            projectileCost += projectileCost * 1/2;
            PlayerPrefs.SetFloat("projectileValue", shooting.bulletForce);
            Debug.Log("Projectile Speed Increased");
        }
        
    }

    public void CurrencyGainIncrease()
    {
        if (currency.count >= currencyCost)
        {
            currency.currencyGain++;
            currency.count -= currencyCost;
            currencyCost += currencyCost * 1/2;
            PlayerPrefs.SetInt("currencyGainValue", currency.currencyGain);
            Debug.Log("Currency Gain Increased");
        }
        
    }

}
