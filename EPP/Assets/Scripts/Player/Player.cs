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
    public GameObject handgunBulletPrefab;
    public GameObject shotgunBulletPrefab;

    public float chipSpeed = 2f;
    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthVar;
    public TextMeshProUGUI healthText;

    public float offset;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("move", false);
    }

    void Update()
    {
        UpdateHealthUI();

        /* Shoot(); */
        
        Movement();

        TestKeys();
        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public void TestKeys()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(15);
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            AddHealth(15);
        }
    }

    public void Movement()
    {
      if (gameManager.handgun == true)
      {
        animator.SetBool("handgun", true);
        animator.SetBool("shotgun", false);
        animator.SetBool("assaultRifle", false);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("move", true);

        } else {
            animator.SetBool("move", false);
        }
      }

      if (gameManager.shotgun == true)
      {
        animator.SetBool("handgun", false);
        animator.SetBool("shotgun", true);
        animator.SetBool("assaultRifle", false);
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("move", true);

        } else {
            animator.SetBool("move", false);
        }
      }

      if (gameManager.AssaultRifle == true)
      {
        animator.SetBool("handgun", false);
        animator.SetBool("shotgun", false);
        animator.SetBool("assaultRifle", true);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("move", true);

        } else {
            animator.SetBool("move", false);
        }
      }
        

        if(gameManager.canMove != false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
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

    public void TakeDamage(float amount)
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

    /* private void OnCollisionStay2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if(Time.time - gameManager.)
            var enemy = collision.GetComponent<Enemy>();

            TakeDamage(enemy.zombieDamage);
        }
    } */

    public void AddHealth(int _value)
    {
        gameManager.playerCurrentHealth = Mathf.Clamp(gameManager.playerCurrentHealth + _value, 0, gameManager.playerMaxHealth);
        lerpTimer = 0f;
    }

    /*void Shoot()
    {
        if(Input.GetButtonDown("Fire1") && gameManager.canShoot == true)
        {
            if (gameManager.handgun == true)
            {
                GameObject handgunBullet = Instantiate(handgunBulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = handgunBullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * gameManager.projectileSpeed, ForceMode2D.Impulse);
            }

            if (gameManager.shotgun == true)
            {
                Quaternion newRot = firePoint.rotation;

                gameManager.handgun = false;
                gameManager.machinePistol = false;
                gameManager.subMachineGun = false;
                gameManager.AssaultRifle = false;

                for (int i = 0; i < gameManager.shotgunAmmo; i++)
                {
                    GameObject s = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = s.GetComponent<Rigidbody2D>();
                    Vector2 dir = transform.rotation * Vector2.up;
                    Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-gameManager.spread, gameManager.spread);
                    rb.velocity = (dir * pdir) * gameManager.projectileSpeed;
                }

                 for (int i = 0; i < gameManager.shotgunAmmo; i++)
                {
                    float addedOffset =  i - (gameManager.shotgunAmmo / 2) *  gameManager.spread;

                    GameObject shotgunBullet = Instantiate(shotgunBulletPrefab, firePoint.position, newRot);

                    newRot = Quaternion.Euler(firePoint.transform.eulerAngles.x, firePoint.transform.eulerAngles.y, firePoint.transform.eulerAngles.z + addedOffset);

                    
                    Rigidbody2D rb = shotgunBullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * gameManager.projectileSpeed,ForceMode2D.Impulse);
                } 

            }
            
        }
        
    } */

    public void UpdateHealthUI()
    {
        
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthVar.fillAmount;
        float hFraction = gameManager.playerCurrentHealth / gameManager.playerMaxHealth;
        
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
