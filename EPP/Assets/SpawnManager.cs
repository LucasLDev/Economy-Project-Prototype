using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static GameObject[] spawnPoints;
    public static string lastScene = "";
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoints = GameObject.FindGameObjectsWithTag("MainAreaSpawns");
        SpawnCheck();
    }

    public void SpawnCheck()
    {
        if(lastScene == "MainScene")
        {
            player.transform.position = spawnPoints[0].transform.position;
        }
    }
}
