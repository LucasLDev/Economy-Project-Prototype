using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float playerDamage = 1;
    public int maxHealth = 3;
    public float currentHealth;

    private bool dead;

    public Slider playerHealth;
 
    void Start()
    {
        currentHealth = maxHealth;
        playerHealth.maxValue = maxHealth;
    }

    void Update()
    {
        playerHealth.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if(currentHealth > 0)
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

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }*/
}
