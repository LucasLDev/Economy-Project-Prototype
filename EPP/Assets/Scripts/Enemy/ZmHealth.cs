using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZmHealth : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject _gameManager;
    public int zombieCurrentHealth;
    public Slider ZombieHealthBar;

    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        gameManager = _gameManager.GetComponent<GameManager>();

        //gameManager.zombieMaxHealth += gameManager.playerMaxHealth * 8/10;
        zombieCurrentHealth = gameManager.zombieMaxHealth;
        ZombieHealthBar.maxValue = gameManager.zombieMaxHealth;
    }

    void Update()
    {
        ZombieHealthBar.value = zombieCurrentHealth;
    }


    public void ZMTakeDamage(int zmAmount)
    {
        zombieCurrentHealth = Mathf.Clamp(zombieCurrentHealth - zmAmount, 0, gameManager.zombieMaxHealth);

        if(zombieCurrentHealth > 0)
        {
            //hurt
        }
        else
        {
            //dead
            if(!gameManager.zombiesDead)
            {
                //death animation
                //anim.SetTrigger("");

                GetComponent<EnemyPatrol>().enabled = false;
                gameManager.currentFuel += Random.Range(gameManager.minfuelGain, gameManager.maxfuelGain);
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
