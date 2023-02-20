using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public StoreMenu store;
    public Health health;
    public PlayerMovement movement;
    public Shooting shooting;
    public Currency currency;
    public void Start()
    {
        Data data = SaveSystem.Load();

        health.maxHealth = data._maxHealth;
        health.playerDamage = data._damage;
        movement.moveSpeed = data._moveSpeed;
        shooting.bulletForce = data._projectileSpeed;
        currency.currencyGain = data._fuelGain;
        currency.count = data._fuelCount;


    }

}
