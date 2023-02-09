using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
    //float setting the players health
    [SerializeField] float health, maxHealth = 150f;
    //reference to health bar slider
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI Hp;

    public GameObject Enemy;

    //On game start health is set to max health
    //sliders max value equal players max health
    private void Start()
    {
        Hp.text = "HP " + health.ToString();
        health = maxHealth;
        maxHealth = healthSlider.maxValue;

        
    }

    //current slider value always equals the health value
    private void Update()
    {
        healthSlider.value = health;
        Hp.text = "HP " + health.ToString();
    }

    public void TakeDamage(float Amount)
    {
        health -= Amount;
        //if players health is less than or equal to zero.
        if(health <= 0)
        {
            //update slider
            healthSlider.value = health;

            DestroyEnemies();
            
            //load wanted scene
            SceneManager.LoadScene("Death");
            
            
        }
    }

    void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        GameObject.Destroy(enemy);
    }

}
