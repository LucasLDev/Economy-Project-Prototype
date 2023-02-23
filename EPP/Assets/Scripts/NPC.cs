using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject player;

    public DialogueTrigger _dialogue;
    public DialogueTriggerReturn _dialogueReturn;
    public DialogueTriggerEnd _dialogueEnd;
    public GameManager gameManager;
    public GameObject interact;
    public GameObject interactor;

    int xvalue;
    int yvalue;

    public bool interactOn;
    public bool deniedFavour;

    void Start()
    {
        gameManager.zombiesSpawned = false;
        interactor.SetActive(true);
        interactOn = false;
    }

    void Update()
    {
         if (GameObject.FindWithTag("Zombie") == null)
        {
            interactor.SetActive(true);

        } else if (GameObject.FindWithTag("Zombie") != null) 
        {
            gameManager.zombiesSpawned = true;
            zombie.GetComponent<Enemy>();
            interactor.SetActive(false);
        }

        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && deniedFavour == false && gameManager.favourCompleted == false && gameManager.remainingZombies <= 0)
        {
            _dialogue.TriggerDialogue();
            gameManager.inDialogue = true;
            gameManager.canMove = false;
        } else if (interactOn == true && Input.GetKeyDown(KeyCode.F) && deniedFavour == true && gameManager.favourCompleted == false & gameManager.remainingZombies <= 0)
        {
            _dialogueReturn.TriggerReturnDialogue();
            gameManager.inDialogue = true;
            gameManager.canMove = false;
        }   else if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.favourCompleted == true & gameManager.remainingZombies <= 0)
        {
            _dialogueEnd.TriggerEndDialogue();
            gameManager.inDialogue = true;
            gameManager.canMove = false;
        }

    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if(gameManager.remainingZombies <= 0)
        {
            interact.SetActive(true);
            interactOn = true;
        }
        

        /*if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && gameManager.zombiesSpawned == false)
        {
            _dialogue.TriggerDialogue();
            
            
            //Talk to NPC
            //Choose if accept or decline
            //gameManager.zombiesSpawned = true;
            //EnemySpawn();
        } */
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interact.SetActive(false); 
        interactOn = false;
    }

    public void EnemySpawn()
    {
        gameManager.remainingZombies = gameManager.numberOfZombies;

        for(int i = 0; i<gameManager.numberOfZombies; i++)
        {
            gameManager.zombiesSpawned = true;
            gameManager.zombiesDead = false;

            xvalue = Random.Range(-13, 4);
            yvalue = Random.Range(-5, 6);

            Instantiate(zombie, new Vector2(xvalue, yvalue), transform.rotation);

            zombie.SetActive(true);
        }
        
        
    }

   
}
