using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
  /*public GameObject player;
    public float speed;
    public float range;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < range)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
    }*/

    private GameObject player;
    //public GameObject safeZone;
    private GameManager gameManager;
    private GameObject _gameManager;

    //[SerializeField] private float damage;

    private float distance;

    public Transform[] moveSpots;
    private int randomSpot;

    

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        _gameManager = GameObject.FindWithTag("GameManager");
        gameManager = _gameManager.GetComponent<GameManager>();
        
       
        gameManager.inSafeZone = false;
        gameManager.zombiePatrolling = true;
        gameManager.zombieChasing = false;
        gameManager.waitTime = gameManager.startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {

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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }*/
        
}
