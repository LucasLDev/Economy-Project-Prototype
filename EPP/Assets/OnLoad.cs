using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoad : MonoBehaviour
{
    public string startPositionName;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        var startPositionObject = GameObject.Find(startPositionName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
