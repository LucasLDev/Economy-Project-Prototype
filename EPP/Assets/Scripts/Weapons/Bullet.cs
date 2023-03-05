using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject _gameManager;
    public GameObject hitEffect;
    private BulletController bulletController;
    private GameObject _bulletController;



    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        gameManager = _gameManager.GetComponent<GameManager>();

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
            if(gameManager.handgun == true)
            {
                Debug.Log("handgun damage");
                collision.GetComponent<Enemy>().ZMTakeDamage(gameManager.handgunDamage);
                Debug.Log(" _handgun damage");
            }

            if(gameManager.machinePistol == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(gameManager.machinePistolDamage);
            }

            if(gameManager.subMachineGun == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(gameManager.subMachineGunDamage);
            }

            if(gameManager.AssaultRifle == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(gameManager.AssaultRifleDamage);
            }

            if(gameManager.shotgun == true)
            {
                collision.GetComponent<Enemy>().ZMTakeDamage(gameManager.shotgunDamage);
            }
            
        }
        
    }

}
