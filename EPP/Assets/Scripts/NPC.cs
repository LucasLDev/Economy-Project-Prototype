using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject player;

    public GameManager gameManager;
    public GameObject interact;
    public GameObject interactor;

    int xvalue;
    int yvalue;

    void Start()
    {
        gameManager.zombiesSpawned = false;
        interactor.SetActive(true);
    }

    void Update()
    {
         if (GameObject.FindWithTag("Zombie") == null)
        {
            gameManager.zombiesSpawned = false;
            interactor.SetActive(true);

        } else if (GameObject.FindWithTag("Zombie") != null) {
            gameManager.zombiesSpawned = true;
            interactor.SetActive(false);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        interact.SetActive(true);

        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && gameManager.zombiesSpawned == false)
        {
            
            //Talk to NPC
            //Choose if accept or decline
            gameManager.zombiesSpawned = true;
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
        for(int i = 0; i<gameManager.numberOfZombies; i++)
        {
            gameManager.zombiesSpawned = true;

            xvalue = Random.Range(-13, 4);
            yvalue = Random.Range(-5, 6);

            Instantiate(zombie, new Vector2(xvalue, yvalue), transform.rotation);

            zombie.SetActive(true);

            gameManager.zombieCurrentHealth = gameManager.zombieMaxHealth;
        }
        
        
    }
}
