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

    public float spreadAngle = 45.0f;

    private float timeSinceLastShot = 0.0f;
    public Transform player;
    public Vector2 direction;
    public Vector2 offset;
    public Slider reloadVisual;
    public GameObject hgIcon;
    public GameObject arIcon;
    public GameObject sgIcon;
    
    // Start is called before the first frame update
    void Start()
    {
        MainUI.mainUI.reloadIdicator.SetActive(false);

        GameManager.gameManager.handgunCurrentAmmo = GameManager.gameManager.handgunMaxAmmo;
        GameManager.gameManager.machineCurrentAmmo = GameManager.gameManager.machineMaxAmmo;
        GameManager.gameManager.subCurrentAmmo = GameManager.gameManager.subMaxAmmo;
        GameManager.gameManager.assaultCurrentAmmo = GameManager.gameManager.assaultMaxAmmo;
        GameManager.gameManager.shotgunCurrentAmmo = GameManager.gameManager.shotgunMaxAmmo;

        GameManager.gameManager.isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        TriggerShoot();

        ReloadCheck();
        

        if(Input.GetKeyDown(KeyCode.R) && GameManager.gameManager.isReloading == false && canReload == true)
        {
            ReloadWeapon();
        }

    }

    void TriggerShoot()
    {
        if (GameManager.gameManager.handgun == true && GameManager.gameManager.handgunCurrentAmmo > 0 && !GameManager.gameManager.isReloading)
        {
            if (Input.GetButtonDown("Fire1") && GameManager.gameManager.canShoot == true && timeSinceLastShot >= GameManager.gameManager.handgunFireRate)
            {
                GameManager.gameManager.handgunCurrentAmmo--;
                timeSinceLastShot = 0.0f;
                Shoot();
                
            }
        }

        if(GameManager.gameManager.shotgun == true && GameManager.gameManager.shotgunCurrentAmmo > 0 && !GameManager.gameManager.isReloading)
        {
            if (Input.GetButtonDown("Fire1") && GameManager.gameManager.canShoot == true && timeSinceLastShot >= GameManager.gameManager.shotgunFireRate)
            {
                GameManager.gameManager.shotgunCurrentAmmo -= GameManager.gameManager.shotgunAmmo;
                timeSinceLastShot = 0.0f;
                Shoot();
            }
        }

        if(GameManager.gameManager.AssaultRifle == true && GameManager.gameManager.assaultCurrentAmmo > 0 && !GameManager.gameManager.isReloading)
        {
            if (Input.GetButton("Fire1") && GameManager.gameManager.canShoot == true && timeSinceLastShot >= GameManager.gameManager.AssaultRifleFireRate)
            {
                GameManager.gameManager.assaultCurrentAmmo--;
                timeSinceLastShot = 0.0f;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        
        
        if (GameManager.gameManager.handgun == true)
        {
            GameManager.gameManager.shotgun = false;
            GameManager.gameManager.machinePistol = false;
            GameManager.gameManager.subMachineGun = false;
            GameManager.gameManager.AssaultRifle = false;
            
            GameObject handgunBullet = Instantiate(handgunBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = handgunBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * GameManager.gameManager.handgunBulletSpeed, ForceMode2D.Impulse);
        }

        if (GameManager.gameManager.AssaultRifle == true)
        {
            GameManager.gameManager.shotgun = false;
            GameManager.gameManager.machinePistol = false;
            GameManager.gameManager.subMachineGun = false;
            GameManager.gameManager.handgun = false;
            
            GameObject assaultBullet = Instantiate(assaultBulletprefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = assaultBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * GameManager.gameManager.handgunBulletSpeed, ForceMode2D.Impulse);
        }

        if (GameManager.gameManager.shotgun == true)
        {
            GameManager.gameManager.handgun = false;
            GameManager.gameManager.machinePistol = false;
            GameManager.gameManager.subMachineGun = false;
            GameManager.gameManager.AssaultRifle = false;

            float angleStep = spreadAngle / (float)(GameManager.gameManager.shotgunAmmo - 1);
            float currentAngle = -spreadAngle / 2.0f;

            for (int i = 0; i < GameManager.gameManager.shotgunAmmo; i++)
            {
                direction = Quaternion.Euler(0.0f, 0.0f, currentAngle) * transform.up;

                GameObject bullet = Instantiate(shotgunBulletPrefab, transform.position, Quaternion.identity);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                bullet.GetComponent<Rigidbody2D>().velocity = direction * GameManager.gameManager.shotgunBulletSpeed;

                Quaternion rotation = Quaternion.FromToRotation(Vector2.up, -direction);
                
                bullet.transform.rotation = rotation;

                currentAngle += angleStep;
            }
        }
        
        
        
            
    }

    public void ReloadWeapon()
    {
        if(!GameManager.gameManager.isReloading)
        {
            GameManager.gameManager.isReloading = true;
            StartCoroutine(ReloadCoroutine());
            reloadVisual.value = 0;
            
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        float timeElapsed = 0;
        if(GameManager.gameManager.handgun == true)
        {
            reloadVisual.maxValue = GameManager.gameManager.handgunReloadTime;
            while (timeElapsed < GameManager.gameManager.handgunReloadTime)
            {
                timeElapsed += Time.deltaTime;
                reloadVisual.value = timeElapsed;
                yield return null;
            }
            //yield return new WaitForSeconds(GameManager.gameManager.handgunReloadTime);
            GameManager.gameManager.handgunCurrentAmmo = GameManager.gameManager.handgunMaxAmmo;
            GameManager.gameManager.isReloading = false;
            reloadVisual.value = 0;
        }
        if(GameManager.gameManager.AssaultRifle == true)
        {
            reloadVisual.maxValue = GameManager.gameManager.assaultReloadTime;
            while (timeElapsed < GameManager.gameManager.assaultReloadTime)
            {
                timeElapsed += Time.deltaTime;
                reloadVisual.value = timeElapsed;
                yield return null;
            }
            //yield return new WaitForSeconds(GameManager.gameManager.assaultReloadTime);
            GameManager.gameManager.assaultCurrentAmmo = GameManager.gameManager.assaultMaxAmmo;
            GameManager.gameManager.isReloading = false;
            reloadVisual.value = 0;
        }
        if(GameManager.gameManager.shotgun == true)
        {
            reloadVisual.maxValue = GameManager.gameManager.shotgunReloadTime;
            while (timeElapsed < GameManager.gameManager.shotgunReloadTime)
            {
                timeElapsed += Time.deltaTime;
                reloadVisual.value = timeElapsed;
                yield return null;
            }
            //yield return new WaitForSeconds(GameManager.gameManager.shotgunReloadTime);
            GameManager.gameManager.shotgunCurrentAmmo = GameManager.gameManager.shotgunMaxAmmo;
            GameManager.gameManager.isReloading = false;
            reloadVisual.value = 0;
        }
        
    }


    public void ReloadCheck()
    {
        //handgun reload check
        if (GameManager.gameManager.handgun == true && GameManager.gameManager.handgunCurrentAmmo != GameManager.gameManager.handgunMaxAmmo)
        {
            canReload = true;
        } else if (GameManager.gameManager.handgun == true && GameManager.gameManager.handgunCurrentAmmo == GameManager.gameManager.handgunMaxAmmo)
        {
            canReload = false;
        }

        //AR reload check
        if (GameManager.gameManager.AssaultRifle == true && GameManager.gameManager.assaultCurrentAmmo != GameManager.gameManager.assaultMaxAmmo)
        {
            canReload = true;
        } else if (GameManager.gameManager.AssaultRifle == true && GameManager.gameManager.assaultCurrentAmmo == GameManager.gameManager.assaultMaxAmmo)
        {
            canReload = false;
        }

        //shotgun reload check
        if (GameManager.gameManager.shotgun == true && GameManager.gameManager.shotgunCurrentAmmo != GameManager.gameManager.shotgunMaxAmmo)
        {
            canReload = true;
        } else if (GameManager.gameManager.shotgun == true && GameManager.gameManager.shotgunCurrentAmmo == GameManager.gameManager.shotgunMaxAmmo)
        {
            canReload = false;
        }
    }
            
        
}
