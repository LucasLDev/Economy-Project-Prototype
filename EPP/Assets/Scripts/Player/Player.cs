using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private bool dead;

    public Rigidbody2D rb;
    public Camera cam;

    private Animator animator;
    
    Vector2 movement;
    Vector2 mousePos;

    public Transform firePoint;
    public GameObject handgunBulletPrefab;
    public GameObject shotgunBulletPrefab;
    private GameObject areaOneSpawn;

    public float offset;

    void Start()
    {   
        if(SpawnManager.lastScene == "MainScene")
        {
            transform.position = SpawnManager.spawnPoints[0].transform.position;
        }
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            areaOneSpawn = GameObject.FindGameObjectWithTag("Spawn");
            transform.position = areaOneSpawn.transform.position;
        }
        animator = GetComponent<Animator>();
        animator.SetBool("move", false);
    }

    void Update()
    {

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
      if (GameManager.gameManager.handgun == true)
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

      if (GameManager.gameManager.shotgun == true)
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

      if (GameManager.gameManager.AssaultRifle == true)
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
        

        if(GameManager.gameManager.canMove != false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * GameManager.gameManager.playerMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;


    }

    public void TakeDamage(float amount)
    {
        GameManager.gameManager.playerCurrentHealth = Mathf.Clamp(GameManager.gameManager.playerCurrentHealth - amount, 0, GameManager.gameManager.playerMaxHealth);

        if(GameManager.gameManager.playerCurrentHealth > 0)
        {
            MainUI.mainUI.hLerpTimer = 0f;
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
                SceneManager.LoadScene("Death");
            }
        }

    }

    /* private void OnCollisionStay2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if(Time.time - GameManager.gameManager.)
            var enemy = collision.GetComponent<Enemy>();

            TakeDamage(enemy.zombieDamage);
        }
    } */

    public void AddHealth(int _value)
    {
        GameManager.gameManager.playerCurrentHealth = Mathf.Clamp(GameManager.gameManager.playerCurrentHealth + _value, 0, GameManager.gameManager.playerMaxHealth);
        MainUI.mainUI.hLerpTimer = 0f;
    }

    /*void Shoot()
    {
        if(Input.GetButtonDown("Fire1") && GameManager.gameManager.canShoot == true)
        {
            if (GameManager.gameManager.handgun == true)
            {
                GameObject handgunBullet = Instantiate(handgunBulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = handgunBullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * GameManager.gameManager.projectileSpeed, ForceMode2D.Impulse);
            }

            if (GameManager.gameManager.shotgun == true)
            {
                Quaternion newRot = firePoint.rotation;

                GameManager.gameManager.handgun = false;
                GameManager.gameManager.machinePistol = false;
                GameManager.gameManager.subMachineGun = false;
                GameManager.gameManager.AssaultRifle = false;

                for (int i = 0; i < GameManager.gameManager.shotgunAmmo; i++)
                {
                    GameObject s = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = s.GetComponent<Rigidbody2D>();
                    Vector2 dir = transform.rotation * Vector2.up;
                    Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-GameManager.gameManager.spread, GameManager.gameManager.spread);
                    rb.velocity = (dir * pdir) * GameManager.gameManager.projectileSpeed;
                }

                 for (int i = 0; i < GameManager.gameManager.shotgunAmmo; i++)
                {
                    float addedOffset =  i - (GameManager.gameManager.shotgunAmmo / 2) *  GameManager.gameManager.spread;

                    GameObject shotgunBullet = Instantiate(shotgunBulletPrefab, firePoint.position, newRot);

                    newRot = Quaternion.Euler(firePoint.transform.eulerAngles.x, firePoint.transform.eulerAngles.y, firePoint.transform.eulerAngles.z + addedOffset);

                    
                    Rigidbody2D rb = shotgunBullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * GameManager.gameManager.projectileSpeed,ForceMode2D.Impulse);
                } 

            }
            
        }
        
    } */


    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }*/
}
