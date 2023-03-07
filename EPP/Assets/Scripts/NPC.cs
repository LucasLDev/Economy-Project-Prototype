using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{

    public Favour[] favour;
    public GameObject favourWindow;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text fuelText;
    
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject player;

    public DialogueTrigger _dialogue;
    public DialogueTriggerReturn _dialogueReturn;
    public DialogueTriggerEnd _dialogueEnd;
    public GameObject interact;
    public GameObject interactor;
    public Animator favourAnimator;
    private GameManager gameManager;
    

    int xvalue;
    int yvalue;

    public bool interactOn;
    public bool deniedFavour;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
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

        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.inDialogue == false && deniedFavour == false && gameManager.favourCompleted == false && gameManager.objectsWithTag.Length <= 0)
        {
            _dialogue.TriggerDialogue();
            gameManager.inDialogue = true;
            gameManager.canMove = false;
        } else if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.inDialogue == false && deniedFavour == true && gameManager.favourCompleted == false & gameManager.objectsWithTag.Length <= 0)
        {
            _dialogueReturn.TriggerReturnDialogue();
            gameManager.inDialogue = true;
            gameManager.canMove = false;
        }   else if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.inDialogue == false && gameManager.favourCompleted == true & gameManager.objectsWithTag.Length <= 0)
        {
            _dialogueEnd.TriggerEndDialogue();
            gameManager.inDialogue = true;
            gameManager.canMove = false;
        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if(gameManager.objectsWithTag.Length <= 0 &&  other.CompareTag("Player"))
        {
            interact.SetActive(true);
            interactOn = true;
        }
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

    public void OpenFavourWindow()
    {
        favourAnimator.SetBool("open", true);

        titleText.SetText("" + favour[0].title);
        descriptionText.SetText("" + favour[0].description);
        fuelText.SetText("" + favour[0].fuelReward);
    }

    public void CloseFavourWindow()
    {
        favourAnimator.SetBool("open", false);
    }

   
}
