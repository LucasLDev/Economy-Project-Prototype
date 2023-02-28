using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public GameManager gameManager;
    public int level;
    public float currentXp;
    public float requiredXp;

    private float lerpTimer;
    private float delayTimer;
    public float chipSpeed = 4f;

    [Header("UI")]
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
    
    void Start()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        requiredXp = CalculateRequiredXp();
        levelText.text = "" + level;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();

        if(Input.GetKeyDown(KeyCode.Equals))
        {
            FlatRateExperienceGain(20);
        }

        if (currentXp > requiredXp)
        {
            LevelUp();
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
        gameManager.playerCurrentHealth = gameManager.playerMaxHealth;
        requiredXp = CalculateRequiredXp();
        //gameManager.zombieSpeed += gameManager.zombieSpeedUpgrade;
        gameManager.zombieMaxHealth += gameManager.zombieHealthUpgrade;
        gameManager.zombieDamage += gameManager.zombieDamageUpgrade;
        levelText.text = "" + level;
        
    }

    private int CalculateRequiredXp()
    {
        int solvedForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solvedForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solvedForRequiredXp / 4;
    }
}
