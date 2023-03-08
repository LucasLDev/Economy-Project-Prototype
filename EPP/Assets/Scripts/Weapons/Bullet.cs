using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private BulletController bulletController;
    private GameObject _bulletController;



    void Start()
    {

        _bulletController = GameObject.FindWithTag("BulletController");
        bulletController = _bulletController.GetComponent<BulletController>();
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            return;
        } else {
            gameObject.SetActive(false);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f);
            Destroy(gameObject);
        }

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Zombie")
        {
            Debug.Log("handgun collision");
            if(GameManager.gameManager.handgun == true)
            {
                Debug.Log("handgun damage");
                collision.GetComponent<Enemy>().ZMTakeDamage(GameManager.gameManager.handgunDamage);
                Debug.Log(" _handgun damage");
            }

            if(GameManager.gameManager.machinePistol == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(GameManager.gameManager.machinePistolDamage);
            }

            if(GameManager.gameManager.subMachineGun == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(GameManager.gameManager.subMachineGunDamage);
            }

            if(GameManager.gameManager.AssaultRifle == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(GameManager.gameManager.AssaultRifleDamage);
            }

            if(GameManager.gameManager.shotgun == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(GameManager.gameManager.shotgunDamage);
            }
            
        }
        
    }

}
