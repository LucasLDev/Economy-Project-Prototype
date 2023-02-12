using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemiesSpawner : MonoBehaviour
{

    //Refrence to the enemy object.
    [SerializeField] GameObject EnemySpawner;

    [SerializeField] GameObject _player;

    //Creates the int varibles.
    int xvalue;
    int yvalue;

   //Bools.
   public bool EnemyKilled;

    //On start spawn enemies
    private void Start()
    {
        EnemyKilled = true;
        EnemySpawn();
    }

    public void EnemySpawn()
    {
        //If enemykilled is true.
        if (EnemyKilled == true)
        {
            //I changed the values.
            //Picks values betweem a given range randomly
            xvalue = Random.Range(-60, 68);
            yvalue = Random.Range(2, 2);

            //spawn an enemy with the generated values
            Instantiate(EnemySpawner, new Vector2(xvalue, yvalue), _player.transform.rotation);

            //reset
            EnemyKilled = false;
            
        }
    }
}
