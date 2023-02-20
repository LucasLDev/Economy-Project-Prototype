using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class Data
{
    [Header("Script References")]
    public Health _healthScript;
    public PlayerMovement _movementScript;
    public Shooting _shootingScript;
    public Currency _currencyScript;

    [Space]

    [Header("Player")]
    public float[] position;

    [Space]

    [Header("Player Stats")]
    public int _maxHealth;
    public float _damage;
    public int _moveSpeed;
    public float _projectileSpeed;
    public int _fuelGain;
    public int _fuelCount;
    public int _medkitPotency;

    [Space]

    [Header("Upgrade Costs")]
    public int _healthCost;
    public int _damageCost;
    public int _speedCost;
    public float _projectileSpeedCost;
    public int _fuelGainCost;
    public int _medkitCost;

    public Data ()
    {
        _maxHealth = _healthScript.maxHealth;
        _damage = _healthScript.playerDamage;
        _moveSpeed = _movementScript.moveSpeed;
        _projectileSpeed = _shootingScript.bulletForce;
        _fuelGain = _currencyScript.currencyGain;
        _fuelCount = _currencyScript.count;

    }
}
