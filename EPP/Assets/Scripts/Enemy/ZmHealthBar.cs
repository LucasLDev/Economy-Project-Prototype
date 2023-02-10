using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZmHealthBar : MonoBehaviour
{
    [SerializeField] private ZmHealth ZombieHealth;
    [SerializeField] private Image zmTotalHealthBar;
    [SerializeField] private Image zmCurrentHealthBar;
     private void Start()
    {
        //totalHealthBar.fillAmount = playerHealth.maxHealth /10;
    }

    // Update is called once per frame
    void Update()
    {
        zmCurrentHealthBar.fillAmount = ZombieHealth.zmCurrentHealth / 10;
    }
}
