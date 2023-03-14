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
    private float distance;
    private int randomSpot;

    float lastAttackTime;

    private Favour favour;


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
        if(GameManager.gameManager.level == 1)
        {
            zombieLevel = Random.Range(1, 3);
        } else {
            zombieLevel = Random.Range(GameManager.gameManager.level + 2, GameManager.gameManager.level - 2);
        }
        
        ZombieScaling();
        zombieLevelText.text = "" + zombieLevel;
        
        GameManager.gameManager.inSafeZone = false;
        GameManager.gameManager.zombiePatrolling = true;
        GameManager.gameManager.zombieChasing = false;
        
        GameManager.gameManager.waitTime = GameManager.gameManager.startWaitTime;
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
            if(Time.time - lastAttackTime < GameManager.gameManager.attackCooldown) return;
            if(GameManager.gameManager.canMove == false) return;
            _player.TakeDamage(zombieDamage);

            lastAttackTime = Time.time;
            
        }
        
    }

    public void ZombieMovement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

         if(distance < GameManager.gameManager.zombieChaseRange && GameManager.gameManager.inSafeZone == false)
       {
            GameManager.gameManager.zombieChasing = true;

       }    else   {

            GameManager.gameManager.zombiePatrolling = true;
            GameManager.gameManager.zombieChasing = false;
       }

       if(GameManager.gameManager.inSafeZone == true)
       {
            GameManager.gameManager.zombiePatrolling = true;
            GameManager.gameManager.zombieChasing = false;
       }

       if (GameManager.gameManager.zombieChasing == true)
       {
            GameManager.gameManager.zombiePatrolling = false;
            zombieSpeed = zombieChaseSpeed;
            Chase();

       }    else if (GameManager.gameManager.zombiePatrolling == true)   {

            GameManager.gameManager.zombieChasing = false;
            zombieSpeed = zombiePatrolSpeed;
            Patrol();
       }
    }

    public void ZombieScaling()
    {
        for (int i = 1; i < zombieLevel; i++)
        {
            zombieMaxHealth += GameManager.gameManager.zombieHealthUpgrade;
            zombieCurrentHealth = zombieMaxHealth;
            zombieDamage += GameManager.gameManager.zombieDamageUpgrade;
            zombieChaseSpeed += GameManager.gameManager.zombieSpeedUpgrade;
            zombiePatrolSpeed += GameManager.gameManager.zombieSpeedUpgrade;

            //GameManager.gameManager.zombieDamageUpgrade++;
            
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
                if(GameManager.gameManager.waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    GameManager.gameManager.waitTime = GameManager.gameManager.startWaitTime;
                } else{
                    GameManager.gameManager.waitTime -= Time.deltaTime;
                }
            }
    }

    public void Chase()
    {
        if(GameManager.gameManager.canMove == false) return;
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
            if(!GameManager.gameManager.zombiesDead)
            {
                //death animation
                //anim.SetTrigger("");

                GetComponent<Enemy>().enabled = false;
                GameManager.gameManager.currentFuel += Random.Range(GameManager.gameManager.minfuelGain, GameManager.gameManager.maxfuelGain);
                FavourGoal.favourGoal.EnemyKilled();
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
