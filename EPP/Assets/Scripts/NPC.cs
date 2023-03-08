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
    

    int xvalue;
    int yvalue;

    public bool interactOn;
    public bool deniedFavour;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameManager.gameManager.zombiesSpawned = false;
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
            GameManager.gameManager.zombiesSpawned = true;
            zombie.GetComponent<Enemy>();
            interactor.SetActive(false);
        }

        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.inDialogue == false && deniedFavour == false && GameManager.gameManager.favourCompleted == false && GameManager.gameManager.objectsWithTag.Length <= 0)
        {
            _dialogue.TriggerDialogue();
            GameManager.gameManager.inDialogue = true;
            GameManager.gameManager.canMove = false;
        } else if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.inDialogue == false && deniedFavour == true && GameManager.gameManager.favourCompleted == false & GameManager.gameManager.objectsWithTag.Length <= 0)
        {
            _dialogueReturn.TriggerReturnDialogue();
            GameManager.gameManager.inDialogue = true;
            GameManager.gameManager.canMove = false;
        }   else if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.inDialogue == false && GameManager.gameManager.favourCompleted == true & GameManager.gameManager.objectsWithTag.Length <= 0)
        {
            _dialogueEnd.TriggerEndDialogue();
            GameManager.gameManager.inDialogue = true;
            GameManager.gameManager.canMove = false;
        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if(GameManager.gameManager.objectsWithTag.Length <= 0 &&  other.CompareTag("Player"))
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
        GameManager.gameManager.remainingZombies = GameManager.gameManager.numberOfZombies;

        for(int i = 0; i<GameManager.gameManager.numberOfZombies; i++)
        {
            GameManager.gameManager.zombiesSpawned = true;
            GameManager.gameManager.zombiesDead = false;

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
