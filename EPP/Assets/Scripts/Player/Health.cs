using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private bool dead;
    public GameManager gameManager;

    public void TakeDamage(int amount)
    {
        gameManager.playerCurrentHealth = Mathf.Clamp(gameManager.playerCurrentHealth - amount, 0, gameManager.playerMaxHealth);

        if(gameManager.playerCurrentHealth > 0)
        {
            //hurt
        }
        else
        {
            //dead
            if(!dead)
            {   
                //death animation
                //anim.SetTrigger("");

                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                SceneManager.LoadScene(1);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            //collision.GetComponent<Health>().
            TakeDamage(gameManager.zombieDamage);
        }
    }

    public void AddHealth(int _value)
    {
        gameManager.playerCurrentHealth = Mathf.Clamp(gameManager.playerCurrentHealth + _value, 0, gameManager.playerMaxHealth);
    }

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }*/
}
