using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZmHealth : MonoBehaviour
{
    public GameManager gameManager;


    public void ZMTakeDamage(int zmAmount)
    {
        gameManager.zombieCurrentHealth = Mathf.Clamp(gameManager.zombieCurrentHealth - zmAmount, 0, gameManager.zombieMaxHealth);

        if(gameManager.zombieCurrentHealth > 0)
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
                gameManager.zombiesDead = true;
                gameManager.currentFuel += gameManager.fuelGain;
                //PlayerPrefs.SetInt("amount", currency.count);
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
