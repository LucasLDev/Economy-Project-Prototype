using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject _gameManager;
    public GameObject hitEffect;

    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        gameManager = _gameManager.GetComponent<GameManager>();
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
            Debug.Log("hit");
            collision.GetComponent<ZmHealth>().ZMTakeDamage(gameManager.playerDamage);
        }
    }
}
