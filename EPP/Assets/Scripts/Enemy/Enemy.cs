using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Patrol")]
    public Transform[] moveSpots;
    public Transform playerTransform;
    public Rigidbody rb;

    private GameObject player;
    private GameManager gameManager;
    private GameObject _gameManager;

    private float distance;
    private int randomSpot;

    [Header("Zombie Health")]

    public int zombieCurrentHealth;
    public Slider ZombieHealthBar;

    Vector2 walkPoint;

    

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        _gameManager = GameObject.FindWithTag("GameManager");
        gameManager = _gameManager.GetComponent<GameManager>();

        zombieCurrentHealth = gameManager.zombieMaxHealth;
        ZombieHealthBar.maxValue = gameManager.zombieMaxHealth;
        
        gameManager.inSafeZone = false;
        gameManager.zombiePatrolling = true;
        gameManager.zombieChasing = false;
        
        gameManager.waitTime = gameManager.startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        
        ZombieHealthBar.value = zombieCurrentHealth;

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

         if(distance < gameManager.zombieChaseRange && gameManager.inSafeZone == false)
       {
            gameManager.zombieChasing = true;

       }    else   {

            gameManager.zombiePatrolling = true;
            gameManager.zombieChasing = false;
       }

       if(gameManager.inSafeZone == true)
       {
            gameManager.zombiePatrolling = true;
            gameManager.zombieChasing = false;
       }

       if (gameManager.zombieChasing == true)
       {
            gameManager.zombiePatrolling = false;
            Chase();

       }    else if (gameManager.zombiePatrolling == true)   {

            gameManager.zombieChasing = false;
            Patrol();
       }

    }

    
    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, gameManager.zombieSpeed * Time.deltaTime);
        //transform.LookAt(moveSpots[randomSpot]);
        //transform.rotation = Quaternion(transform.position - moveSpots[randomSpot]);

            if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if(gameManager.waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    gameManager.waitTime = gameManager.startWaitTime;
                } else{
                    gameManager.waitTime -= Time.deltaTime;
                }
            }
    }

    public void Chase()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, gameManager.zombieSpeed * Time.deltaTime);
        
    }

    public void ZMTakeDamage(int zmAmount)
    {
        zombieCurrentHealth = Mathf.Clamp(zombieCurrentHealth - zmAmount, 0, gameManager.zombieMaxHealth);

        if(zombieCurrentHealth > 0)
        {
            //hurt
        }
        else
        {
            //dead
            if(!gameManager.zombiesDead)
            {
                //death animation
                //anim.SetTrigger("");

                GetComponent<Enemy>().enabled = false;
                gameManager.currentFuel += Random.Range(gameManager.minfuelGain, gameManager.maxfuelGain);
                gameManager.remainingZombies --;
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }*/
        
}
