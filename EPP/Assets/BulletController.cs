using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject handgunBulletPrefab;
    public GameObject assaultBulletprefab;
    public GameObject shotgunBulletPrefab;
    public GameManager gameManager;

    public float spreadAngle = 45.0f;

    private float timeSinceLastShot = 0.0f;
    public Transform player;
    public Vector2 direction;
    public Vector2 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        TriggerShoot();

    }

    void TriggerShoot()
    {
        if (gameManager.handgun == true)
        {
            if (Input.GetButtonDown("Fire1") && gameManager.canShoot == true && timeSinceLastShot >= gameManager.handgunFireRate)
            {
                timeSinceLastShot = 0.0f;
                Shoot();
                
            }
        }

        if(gameManager.shotgun == true)
        {
            if (Input.GetButtonDown("Fire1") && gameManager.canShoot == true && timeSinceLastShot >= gameManager.shotgunFireRate)
            {
                timeSinceLastShot = 0.0f;
                Shoot();
            }
        }

        if(gameManager.AssaultRifle == true)
        {
            if (Input.GetButton("Fire1") && gameManager.canShoot == true && timeSinceLastShot >= gameManager.AssaultRifleFireRate)
            {
                timeSinceLastShot = 0.0f;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        
        
        if (gameManager.handgun == true)
        {
            gameManager.shotgun = false;
            gameManager.machinePistol = false;
            gameManager.subMachineGun = false;
            gameManager.AssaultRifle = false;
            
            GameObject handgunBullet = Instantiate(handgunBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = handgunBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * gameManager.handgunBulletSpeed, ForceMode2D.Impulse);
        }

        if (gameManager.AssaultRifle == true)
        {
            gameManager.shotgun = false;
            gameManager.machinePistol = false;
            gameManager.subMachineGun = false;
            gameManager.handgun = false;
            
            GameObject assaultBullet = Instantiate(assaultBulletprefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = assaultBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * gameManager.handgunBulletSpeed, ForceMode2D.Impulse);
        }

        if (gameManager.shotgun == true)
        {
            gameManager.handgun = false;
            gameManager.machinePistol = false;
            gameManager.subMachineGun = false;
            gameManager.AssaultRifle = false;

            float angleStep = spreadAngle / (float)(gameManager.shotgunAmmo - 1);
            float currentAngle = -spreadAngle / 2.0f;

            for (int i = 0; i < gameManager.shotgunAmmo; i++)
            {
                direction = Quaternion.Euler(0.0f, 0.0f, currentAngle) * transform.up;

                GameObject bullet = Instantiate(shotgunBulletPrefab, transform.position, Quaternion.identity);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                bullet.GetComponent<Rigidbody2D>().velocity = direction * gameManager.shotgunBulletSpeed;

                Quaternion rotation = Quaternion.FromToRotation(Vector2.up, -direction);
                
                bullet.transform.rotation = rotation;

                currentAngle += angleStep;
            }
        }
        
        
        
            
    }
    
        
            
        
        
    
}
