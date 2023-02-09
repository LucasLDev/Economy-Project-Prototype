using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{

    private bool quitting = false;

    //refence to enemy spawner script
    [SerializeField] EnemiesSpawner _enemySpawn;
   
    //on start find the enemy spawner
    private void Start()
    {
        _enemySpawn = FindObjectOfType<EnemiesSpawner>();
    }

    //if quitting set quitting to true
    private void OnApplicationQuit()
    {
        quitting = true;
    }


    //When enemy is destroyed spawn a new enemy
    //Added a timer that counts down after killing an enemy before the object is destroyed
    private void OnDestroy()
    {
        //if application is not quitting then Spawn enemy on destroy
        if (!quitting)
        {
            _enemySpawn.EnemyKilled = true;
            _enemySpawn.EnemySpawn();
            //destroy object after 1 second
            Destroy(gameObject);
        }
    }


}
