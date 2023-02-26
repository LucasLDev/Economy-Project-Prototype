using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private bool dead;

    public GameManager gameManager;

    public Rigidbody2D rb;
    public Camera cam;

    private Animator animator;
    
    Vector2 movement;
    Vector2 mousePos;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float chipSpeed = 2f;
    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthVar;
    public TextMeshProUGUI healthText;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("move", false);
    }

    void Update()
    {
        UpdateHealthUI();

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        //input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("move", true);
        } else {
            animator.SetBool("move", false);
        }

        if(gameManager.canMove != false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(15);
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            AddHealth(15);
        }
    }

    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * gameManager.playerMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;


    }

    public void TakeDamage(int amount)
    {
        gameManager.playerCurrentHealth = Mathf.Clamp(gameManager.playerCurrentHealth - amount, 0, gameManager.playerMaxHealth);

        if(gameManager.playerCurrentHealth > 0)
        {
            lerpTimer = 0f;
        }
        else
        {
            //dead
            if(!dead)
            {   
                //death animation
                //anim.SetTrigger("");
  
                GetComponent<Player>().enabled = false;
                dead = true;
                SceneManager.LoadScene(2);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            //collision.GetComponent<Health>().
            TakeDamage(gameManager.zombieDamage);
        }
    }

    public void AddHealth(int _value)
    {
        gameManager.playerCurrentHealth = Mathf.Clamp(gameManager.playerCurrentHealth + _value, 0, gameManager.playerMaxHealth);
        lerpTimer = 0f;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * gameManager.projectileSpeed, ForceMode2D.Impulse);

    }

    public void UpdateHealthUI()
    {
        
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthVar.fillAmount;
        float hFraction = gameManager.playerCurrentHealth / gameManager.playerMaxHealth;
        Debug.Log(hFraction);
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthVar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthVar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthVar.color = Color.green;
            backHealthVar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthVar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(gameManager.playerCurrentHealth) + "/" + Mathf.Round(gameManager.playerMaxHealth);
    }

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }*/
}
