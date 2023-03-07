using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Favour
{

    public bool isActive;
    public string title;

    [TextArea(5, 15)]
    public string description;
    public int fuelReward;
}
