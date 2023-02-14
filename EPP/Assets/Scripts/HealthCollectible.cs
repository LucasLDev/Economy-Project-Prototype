using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
   [SerializeField] private float healthValue;
   public Health _health;

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.tag == "Player" && _health.currentHealth != _health.maxHealth)
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
   }
}
