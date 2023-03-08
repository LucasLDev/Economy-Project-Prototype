using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCheck : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject _gameManager;
    void Start()
    {
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
             GameManager.gameManager.inSafeZone = true;
        } 
        

    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.gameManager.inSafeZone = false;
        }
        
    }

    
}
