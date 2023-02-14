using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZmHealth : MonoBehaviour
{

    public float zmMaxHealth = 5;
    public float zmCurrentHealth;
    public Slider enemySlider;

    private Currency currency;
    private GameObject _currency;
    private Health health;
    private GameObject _health;

    private bool zmDead;

    // Start is called before the first frame update
    void Start()
    {
        _currency = GameObject.FindWithTag("GameManager");
        currency = _currency.GetComponent<Currency>();

        _health = GameObject.FindWithTag("Player");
        health = _health.GetComponent<Health>();

        zmMaxHealth += health.maxHealth * 9/10;
        zmCurrentHealth = zmMaxHealth;
        enemySlider.maxValue = zmMaxHealth;
    }

    void Update()
    {
        enemySlider.value = zmCurrentHealth;
        
    }

    public void ZMTakeDamage(float zmAmount)
    {
        zmCurrentHealth = Mathf.Clamp(zmCurrentHealth - zmAmount, 0, zmMaxHealth);

        if(zmCurrentHealth > 0)
        {
            //hurt
        }
        else
        {
            //dead
            if(!zmDead)
            {
                //death animation
                //anim.SetTrigger("");

                GetComponent<EnemyPatrol>().enabled = false;
                zmDead = true;
                currency.count += currency.currencyGain;
                PlayerPrefs.SetInt("amount", currency.count);
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
