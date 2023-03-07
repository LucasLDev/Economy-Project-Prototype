using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public bool canReload;
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
    public Slider reloadVisual;
    public GameObject reloadIdicator;
    public GameObject hgIcon;
    public GameObject arIcon;
    public GameObject sgIcon;
    
    // Start is called before the first frame update
    void Start()
    {
        reloadIdicator.SetActive(false);

        gameManager.handgunCurrentAmmo = gameManager.handgunMaxAmmo;
        gameManager.machineCurrentAmmo = gameManager.machineMaxAmmo;
        gameManager.subCurrentAmmo = gameManager.subMaxAmmo;
        gameManager.assaultCurrentAmmo = gameManager.assaultMaxAmmo;
        gameManager.shotgunCurrentAmmo = gameManager.shotgunMaxAmmo;

        gameManager.isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        TriggerShoot();
        
        ReloadIndicator();
        
        IconCheck();

        ReloadCheck();
        

        if(Input.GetKeyDown(KeyCode.R) && gameManager.isReloading == false && canReload == true)
        {
            ReloadWeapon();
        }

    }

    void TriggerShoot()
    {
        if (gameManager.handgun == true && gameManager.handgunCurrentAmmo > 0 && !gameManager.isReloading)
        {
            if (Input.GetButtonDown("Fire1") && gameManager.canShoot == true && timeSinceLastShot >= gameManager.handgunFireRate)
            {
                gameManager.handgunCurrentAmmo--;
                timeSinceLastShot = 0.0f;
                Shoot();
                
            }
        }

        if(gameManager.shotgun == true && gameManager.shotgunCurrentAmmo > 0 && !gameManager.isReloading)
        {
            if (Input.GetButtonDown("Fire1") && gameManager.canShoot == true && timeSinceLastShot >= gameManager.shotgunFireRate)
            {
                gameManager.shotgunCurrentAmmo -= gameManager.shotgunAmmo;
                timeSinceLastShot = 0.0f;
                Shoot();
            }
        }

        if(gameManager.AssaultRifle == true && gameManager.assaultCurrentAmmo > 0 && !gameManager.isReloading)
        {
            if (Input.GetButton("Fire1") && gameManager.canShoot == true && timeSinceLastShot >= gameManager.AssaultRifleFireRate)
            {
                gameManager.assaultCurrentAmmo--;
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

    public void ReloadWeapon()
    {
        if(!gameManager.isReloading)
        {
            gameManager.isReloading = true;
            StartCoroutine(ReloadCoroutine());
            reloadVisual.value = 0;
            
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        float timeElapsed = 0;
        if(gameManager.handgun == true)
        {
            reloadVisual.maxValue = gameManager.handgunReloadTime;
            while (timeElapsed < gameManager.handgunReloadTime)
            {
                timeElapsed += Time.deltaTime;
                reloadVisual.value = timeElapsed;
                yield return null;
            }
            //yield return new WaitForSeconds(gameManager.handgunReloadTime);
            gameManager.handgunCurrentAmmo = gameManager.handgunMaxAmmo;
            gameManager.isReloading = false;
            reloadVisual.value = 0;
        }
        if(gameManager.AssaultRifle == true)
        {
            reloadVisual.maxValue = gameManager.assaultReloadTime;
            while (timeElapsed < gameManager.assaultReloadTime)
            {
                timeElapsed += Time.deltaTime;
                reloadVisual.value = timeElapsed;
                yield return null;
            }
            //yield return new WaitForSeconds(gameManager.assaultReloadTime);
            gameManager.assaultCurrentAmmo = gameManager.assaultMaxAmmo;
            gameManager.isReloading = false;
            reloadVisual.value = 0;
        }
        if(gameManager.shotgun == true)
        {
            reloadVisual.maxValue = gameManager.shotgunReloadTime;
            while (timeElapsed < gameManager.shotgunReloadTime)
            {
                timeElapsed += Time.deltaTime;
                reloadVisual.value = timeElapsed;
                yield return null;
            }
            //yield return new WaitForSeconds(gameManager.shotgunReloadTime);
            gameManager.shotgunCurrentAmmo = gameManager.shotgunMaxAmmo;
            gameManager.isReloading = false;
            reloadVisual.value = 0;
        }
        
    }
    
    public void ReloadIndicator()
    {
        if (gameManager.handgun == true && gameManager.handgunCurrentAmmo == 0)
        {
            reloadIdicator.SetActive(true);

        } else if (gameManager.handgun == true && gameManager.handgunCurrentAmmo > 0)
        {
            reloadIdicator.SetActive(false);
        }

        if (gameManager.shotgun == true && gameManager.shotgunCurrentAmmo == 0)
        {
            reloadIdicator.SetActive(true);

        } else if (gameManager.shotgun == true && gameManager.shotgunCurrentAmmo > 0)
        {
            reloadIdicator.SetActive(false);
        }

        if (gameManager.AssaultRifle == true && gameManager.assaultCurrentAmmo == 0)
        {
            reloadIdicator.SetActive(true);

        } else if(gameManager.AssaultRifle == true && gameManager.assaultCurrentAmmo > 0)
        {
            reloadIdicator.SetActive(false);
        }
    }

    public void IconCheck()
    {
        if (gameManager.handgun == true)
        {
            hgIcon.SetActive(true);
            arIcon.SetActive(false);
            sgIcon.SetActive(false);
        }
        if (gameManager.AssaultRifle == true)
        {
            hgIcon.SetActive(false);
            arIcon.SetActive(true);
            sgIcon.SetActive(false);
        }
        if (gameManager.shotgun == true)
        {
            hgIcon.SetActive(false);
            arIcon.SetActive(false);
            sgIcon.SetActive(true);
        }
    }

    public void ReloadCheck()
    {
        //handgun reload check
        if (gameManager.handgun == true && gameManager.handgunCurrentAmmo != gameManager.handgunMaxAmmo)
        {
            canReload = true;
        } else if (gameManager.handgun == true && gameManager.handgunCurrentAmmo == gameManager.handgunMaxAmmo)
        {
            canReload = false;
        }

        //AR reload check
        if (gameManager.AssaultRifle == true && gameManager.assaultCurrentAmmo != gameManager.assaultMaxAmmo)
        {
            canReload = true;
        } else if (gameManager.AssaultRifle == true && gameManager.assaultCurrentAmmo == gameManager.assaultMaxAmmo)
        {
            canReload = false;
        }

        //shotgun reload check
        if (gameManager.shotgun == true && gameManager.shotgunCurrentAmmo != gameManager.shotgunMaxAmmo)
        {
            canReload = true;
        } else if (gameManager.shotgun == true && gameManager.shotgunCurrentAmmo == gameManager.shotgunMaxAmmo)
        {
            canReload = false;
        }
    }
            
        
}
