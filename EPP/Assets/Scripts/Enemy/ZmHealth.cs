using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZmHealth : MonoBehaviour
{

    public float zmMaxHealth = 5;
    public float zmCurrentHealth;
    public Slider enemySlider;

    private bool zmDead;

    // Start is called before the first frame update
    void Start()
    {
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
                Destroy(gameObject);
            }
        }
    }
}
