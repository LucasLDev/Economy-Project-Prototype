using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    private Health pHealth;
    private GameObject _pHealth;

    void Start()
    {
        _pHealth = GameObject.FindWithTag("Player");
        pHealth = _pHealth.GetComponent<Health>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            //Debug.Log("hit");
            collision.GetComponent<ZmHealth>().ZMTakeDamage(pHealth.playerDamage);
        }
    }
}
