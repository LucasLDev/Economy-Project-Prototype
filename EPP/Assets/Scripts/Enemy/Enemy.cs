using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Patrol")]
    public GameObject[] moveSpots;
    public Transform playerTransform;
    public Rigidbody2D rb;
    public TextMeshProUGUI zombieLevelText;

    private GameObject player;
    public Player _player;
    private GameManager GameManager;
    private GameObject _GameManager; 
    private float distance;
    private int randomSpot;

    float lastAttackTime;


    [Header("Zombie Stats")]
    [Space]
    public float zombieMaxHealth = 5f;
    public float zombieDamage = 1;
    public float zombieSpeed = 1;
    public float zombieChaseSpeed;
    public float zombiePatrolSpeed;
    public int zombieLevel;

    public float zombieCurrentHealth;
    public Slider ZombieHealthBar;

    Vector2 walkPoint;

    public float chipSpeed = 2f;
    private float lerpTimer;
    public Image currentHealthBar;
    public Image TakeDamageHealthBar;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _player = player.GetComponent<Player>();

        rb = this.GetComponent<Rigidbody2D>();

        zombieCurrentHealth = zombieMaxHealth;
        ZombieHealthBar.maxValue = zombieMaxHealth;
        if(GameManager.level == 1)
        {
            zombieLevel = Random.Range(1, 3);
        } else {
            zombieLevel = Random.Range(GameManager.level + 2, GameManager.level - 2);
        }
        
        ZombieScaling();
        zombieLevelText.text = "" + zombieLevel;
        
        GameManager.inSafeZone = false;
        GameManager.zombiePatrolling = true;
        GameManager.zombieChasing = false;
        
        GameManager.waitTime = GameManager.startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        moveSpots = GameObject.FindGameObjectsWithTag("MoveSpots");
        UpdateZombieHealthUI();
        
        ZombieHealthBar.value = zombieCurrentHealth;

        ZombieMovement();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(Time.time - lastAttackTime < GameManager.attackCooldown) return;

            _player.TakeDamage(zombieDamage);

            lastAttackTime = Time.time;
            
        }
        
    }

    public void ZombieMovement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

         if(distance < GameManager.zombieChaseRange && GameManager.inSafeZone == false)
       {
            GameManager.zombieChasing = true;

       }    else   {

            GameManager.zombiePatrolling = true;
            GameManager.zombieChasing = false;
       }

       if(GameManager.inSafeZone == true)
       {
            GameManager.zombiePatrolling = true;
            GameManager.zombieChasing = false;
       }

       if (GameManager.zombieChasing == true)
       {
            GameManager.zombiePatrolling = false;
            zombieSpeed = zombieChaseSpeed;
            Chase();

       }    else if (GameManager.zombiePatrolling == true)   {

            GameManager.zombieChasing = false;
            zombieSpeed = zombiePatrolSpeed;
            Patrol();
       }
    }

    public void ZombieScaling()
    {
        for (int i = 1; i < zombieLevel; i++)
        {
            zombieMaxHealth += GameManager.zombieHealthUpgrade;
            zombieCurrentHealth = zombieMaxHealth;
            zombieDamage += GameManager.zombieDamageUpgrade;
            zombieChaseSpeed += GameManager.zombieSpeedUpgrade;
            zombiePatrolSpeed += GameManager.zombieSpeedUpgrade;

            //GameManager.zombieDamageUpgrade++;
            
        }
        
    }

    
    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].transform.position, zombieSpeed * Time.deltaTime);
        Vector3 direction = moveSpots[randomSpot].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        //transform.LookAt(moveSpots[randomSpot]);
        //transform.rotation = Quaternion(transform.position - moveSpots[randomSpot]);

            if(Vector2.Distance(transform.position, moveSpots[randomSpot].transform.position) < 0.2f)
            {
                if(GameManager.waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    GameManager.waitTime = GameManager.startWaitTime;
                } else{
                    GameManager.waitTime -= Time.deltaTime;
                }
            }
    }

    public void Chase()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, zombieSpeed * Time.deltaTime);
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        
    }

    public void ZMTakeDamage(float zmAmount)
    {
        zombieCurrentHealth = Mathf.Clamp(zombieCurrentHealth - zmAmount, 0, zombieMaxHealth);

        if(zombieCurrentHealth > 0)
        {
            //hurt
        }
        else
        {
            //dead
            if(!GameManager.zombiesDead)
            {
                //death animation
                //anim.SetTrigger("");

                GetComponent<Enemy>().enabled = false;
                GameManager.currentFuel += Random.Range(GameManager.minfuelGain, GameManager.maxfuelGain);
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    void UpdateZombieHealthUI()
    {
        float fillF = currentHealthBar.fillAmount;
        float fillB = TakeDamageHealthBar.fillAmount;
        float hFraction = zombieCurrentHealth / zombieMaxHealth;

        if (fillB > hFraction)
        {
            currentHealthBar.fillAmount = hFraction;
            TakeDamageHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            TakeDamageHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        /*if(fillF < hFraction)
        {
            TakeDamageHealthBar.color = Color.green;
            TakeDamageHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            currentHealthBar.fillAmount = Mathf.Lerp(fillF, TakeDamageHealthBar.fillAmount, percentComplete);
        }*/
    }

        
}
