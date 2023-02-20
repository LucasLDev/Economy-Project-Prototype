using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject player;

    public ZmHealth _zmhealth;
    public GameObject interact;

    public bool zombiesSpawned;


    public int numberOfZombies;
    int xvalue;
    int yvalue;

    void Start()
    {
        zombiesSpawned = false;
    }

    void Update()
    {
         if (GameObject.FindWithTag("Zombie") == null)
        {
            zombiesSpawned = false;

        } else if (GameObject.FindWithTag("Zombie") != null) {
            zombiesSpawned = true;
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        interact.SetActive(true);

        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && zombiesSpawned == false)
        {
            
            //Talk to NPC
            //Choose if accept or decline
            zombiesSpawned = true;
            EnemySpawn();
            Debug.Log("Zombies Spawned");
        } 
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interact.SetActive(false); 
    }

    public void EnemySpawn()
    {
        for(int i = 0; i<numberOfZombies; i++)
        {
            zombiesSpawned = true;

            xvalue = Random.Range(-13, 4);
            yvalue = Random.Range(-5, 6);

            Instantiate(zombie, new Vector2(xvalue, yvalue), transform.rotation);

            zombie.SetActive(true);

            _zmhealth.zmCurrentHealth = _zmhealth.zmMaxHealth;
        }
        
        
    }
}
