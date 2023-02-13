using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCheck : MonoBehaviour
{
    public bool inZone;

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inZone = true;
        } 
        

    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
        
    }

    
}
