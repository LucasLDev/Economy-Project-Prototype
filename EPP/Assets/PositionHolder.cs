using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHolder : MonoBehaviour
{
    public GameObject enemy;
     public float canvasOffset;
 
     // Update is called once per frame
     void Update()
     {
        transform.localScale = new Vector3(1, 1, 1);
         transform.eulerAngles = new Vector3(0, 0, 0);
         transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + canvasOffset, 0);
     }
}
