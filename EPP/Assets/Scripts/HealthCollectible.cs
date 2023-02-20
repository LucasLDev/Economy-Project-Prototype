using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
   public GameManager gameManager;

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.tag == "Player" && gameManager.playerCurrentHealth != gameManager.playerMaxHealth)
        {
            collision.GetComponent<Health>().AddHealth(gameManager.medkitPotency);
            gameObject.SetActive(false);
        }
   }
}
