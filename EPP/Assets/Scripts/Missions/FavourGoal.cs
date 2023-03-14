using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FavourGoal
{
    public static FavourGoal favourGoal;
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if(goalType == GoalType.Clear)
        {
            currentAmount++;
        }
    }

    public void ItemCollected()
    {
        if(goalType == GoalType.Retrieve)
        {
            currentAmount++;
        }
    }
}

public enum GoalType
{
    Clear,
    Retrieve,
    Defend,
    Rescue

}
