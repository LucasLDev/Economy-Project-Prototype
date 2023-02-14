using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Health _health;
    public PlayerMovement _movement;
    public Shooting _shooting;
    public Currency _currency;

    public TMP_Text healthStat;
    public TMP_Text dmgStat;
    public TMP_Text speedStat;
    public TMP_Text projectileSpeedStat;
    public TMP_Text currencyGainStat;
    
    void Start()
    {
        
    }

    void Update()
    {
         healthStat.SetText("" + _health.maxHealth);
         dmgStat.SetText("" + _health.playerDamage);
         speedStat.SetText("" + _movement.moveSpeed);
         projectileSpeedStat.SetText("" + _shooting.bulletForce);
         currencyGainStat.SetText("" + _currency.currencyGain);
    }
}
