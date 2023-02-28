using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Patrol")]
    public Transform[] moveSpots;
    public Transform playerTransform;
    public Rigidbody2D rb;
    public TextMeshProUGUI zombieLevelText;

    private GameObject player;
    private GameManager gameManager;
    private GameObject _gameManager; 
    private float distance;
    private int randomSpot;

    [Header("Zombie Health")]

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

        rb = this.GetComponent<Rigidbody2D>();

        _gameManager = GameObject.FindWithTag("GameManager");
        gameManager = _gameManager.GetComponent<GameManager>();

        zombieCurrentHealth = gameManager.zombieMaxHealth;
        ZombieHealthBar.maxValue = gameManager.zombieMaxHealth;
        gameManager.zombieLevel = gameManager.level;
        zombieLevelText.text = "" + gameManager.zombieLevel;
        
        gameManager.inSafeZone = false;
        gameManager.zombiePatrolling = true;
        gameManager.zombieChasing = false;
        
        gameManager.waitTime = gameManager.startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        UpdateZombieHealthUI();
        
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
            gameManager.zombieSpeed = gameManager.zombieChaseSpeed;
            Chase();

       }    else if (gameManager.zombiePatrolling == true)   {

            gameManager.zombieChasing = false;
            gameManager.zombieSpeed = gameManager.zombiePatrolSpeed;
            Patrol();
       }

    }

    
    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, gameManager.zombieSpeed * Time.deltaTime);
        Vector3 direction = moveSpots[randomSpot].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
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
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        
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

    void UpdateZombieHealthUI()
    {
        float fillF = currentHealthBar.fillAmount;
        float fillB = TakeDamageHealthBar.fillAmount;
        float hFraction = zombieCurrentHealth / gameManager.zombieMaxHealth;
        Debug.Log(hFraction);
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

    void ZombieLevelUp()
    {
        for (int i = 1; i <= gameManager.zombieLevel; i++)
        {
            gameManager.zombieSpeed += gameManager.zombieSpeedUpgrade;
            gameManager.zombieMaxHealth += gameManager.zombieHealthUpgrade;
            gameManager.zombieDamage += gameManager.zombieDamageUpgrade;
        }


    }

        
}
