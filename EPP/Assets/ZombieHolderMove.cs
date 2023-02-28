using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHolderMove : MonoBehaviour
{
    public GameObject zombie;

    void Update()
    {
        transform.position = zombie.transform.position;
    }
}
