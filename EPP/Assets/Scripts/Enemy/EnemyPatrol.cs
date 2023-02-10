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

    public GameObject player;
    //public GameObject safeZone;

    public ZoneCheck _zone;

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

    

    void Start()
    {
        _zone.inZone = false;
        patrol = true;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (_zone.inZone == true)
        {
            chase = false;
            Debug.Log("ChaseOff");
            patrol = true;
        }

       if(distance < range & _zone.inZone == false)
       {
            chase = true;
            patrol = false;
       } else {
            patrol = true;
            chase = false;
       }
        

        if (chase == true)
        {
            patrol = false;
            Chase();
        } else if (patrol == true)
        {
            chase = false;
            Patrol();
        }
        
    }


    public void Patrol()
    {
    
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
