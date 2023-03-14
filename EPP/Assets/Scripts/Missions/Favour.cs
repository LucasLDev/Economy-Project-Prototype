using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Favour
{

    public bool isActive;
    public string location;
    public string title;

    [TextArea(5, 15)]
    public string description;
    public int fuelReward;

    public FavourGoal goal;

    public void Complete()
    {
        isActive = false;
        GameManager.gameManager.currentFuel += fuelReward;
    }
    
}
