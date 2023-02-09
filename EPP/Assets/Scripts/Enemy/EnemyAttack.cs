using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //On enemy collision with player
    private void OnCollisionEnter(Collision collision)
    {
        //gets player health component and takes 25 damage
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth PlayerComponent))
        {
            PlayerComponent.TakeDamage(25);
        }
    }
}
