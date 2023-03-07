using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static GameObject[] spawnPoints;
    public static string lastScene = "";

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("MainAreaSpawns");
    }
}
