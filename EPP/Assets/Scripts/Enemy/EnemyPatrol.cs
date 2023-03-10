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

    private ZoneCheck _zone;
    private GameObject zoneTrigger;

    [SerializeField] private float damage;

    public bool chase;
    public bool patrol;

    public float speed;
    public float range;
    private float distance;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;


    public bool inZone;

    

    void Start()
    {
       // _zone = GameObject.FindWithTag("Trigger");
       // _zone.GetComponent<ZoneCheck>();
       zoneTrigger = GameObject.FindWithTag("Trigger");
       _zone = zoneTrigger.GetComponent<ZoneCheck>();
        player = GameObject.FindWithTag("Player");
        _zone.inZone = true;
        patrol = true;
        chase = false;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

         if(distance < range && _zone.inZone == false)
       {
            chase = true;

       }    else   {

            patrol = true;
       }

       if(_zone.inZone == true)
       {
            patrol = true;
            chase = false;
       }

       if (chase == true)
       {
            patrol = false;
            Chase();

       }    else if (patrol == true)   {

            chase = false;
            Patrol();
       }
        
        
    }


    public void Patrol()
    {
    Debug.Log("Patrolling");
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if(waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                } else{
                    waitTime -= Time.deltaTime;
                }
            }
    }

    public void Chase()
    {
            Debug.Log("Chasing");
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
        
}
