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
    public GameObject safeZone;

    ZoneCheck _zone;

    public bool chase;
    public bool patrol;
    public bool inZone;

    public float speed;
    public float range;
    private float distance;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    void Start()
    {
        inZone = false;
        patrol = true;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (inZone == true)
        {
            chase = false;
            patrol = true;
        }

       if(distance < range)
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

    
     void OnTriggerEnter2D(Collider2D safeZone)
    {
        if (safeZone.gameObject.CompareTag("Player"))
        {
            inZone = true;
            Debug.Log("Safe Zone Enter");
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            inZone = false;
            Debug.Log("Safe Zone Exit");
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
        
}
