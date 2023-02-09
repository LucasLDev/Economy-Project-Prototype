using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth, maxHealth;

    public Slider enemySlider;

    void Start()
    {
        enemyHealth = maxHealth;
        maxHealth = enemySlider.maxValue;
    }
    void Update()
    {
        enemySlider.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (enemyHealth > maxHealth)
        {
            enemyHealth = maxHealth;
        }
    }

}
