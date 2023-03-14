using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavourGiver : MonoBehaviour
{
    public static FavourGiver giveFavour;
    public Favour favour;
    public CurrentFavour currentFavour;

    void Awake()
    {
        if(giveFavour == null)
        {
            giveFavour = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    }
    
    public void OpenFavourWindow()
    {
        DialogueManager._dialogue.favourAnimator.SetBool("open", true);

        MainUI.mainUI.titleText.text = "" + favour.title;
        MainUI.mainUI.descriptionText.SetText("" + favour.description);
        MainUI.mainUI.fuelText.SetText("" + favour.fuelReward);
    }

    public void AcceptFavour()
    {
        DialogueManager._dialogue.CloseFavourWindow();
        favour.isActive = true;
        //give qust to player
        currentFavour.favour = favour;
        //NPC._npc.EnemySpawn();
        //GameManager.gameManager.zombiesSpawned = true;
        DialogueManager._dialogue.EndDialogue();
        
    }
}
