using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.tag == "Player" && GameManager.gameManager.playerCurrentHealth != GameManager.gameManager.playerMaxHealth)
        {
            collision.GetComponent<Player>().AddHealth(GameManager.gameManager.medkitPotency);
            gameObject.SetActive(false);
        }
   }
}
