using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _dialogue;
    private Queue<string> sentences;
    [Space]


    [Space]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [Space]
    public Animator animator;
    public Animator favourAnimator;
    [Space]
    public GameObject dialogueBox;
    public GameObject favourScreen;
    //public GameObject yesButton;
    //public GameObject noButton;
    public GameObject continueButton;
    public GameObject acceptButton;
    public GameObject favourButton;
    private GameObject _npc;
    private NPC npc;

   /*  private void Awake()
    {
        if(_dialogue == null)
        {
            _dialogue = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    } */

    // Start is called before the first frame update
    void Start()
    {
        _npc = GameObject.FindGameObjectWithTag("NPC");
        npc = _npc.GetComponent<NPC>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen",true);
        //yesButton.SetActive(false);
        //noButton.SetActive(false);
        //acceptButton.SetActive(false);
        favourButton.SetActive(false);
        continueButton.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 1 && GameManager.gameManager.favourCompleted == false)
        {
            continueButton.SetActive(true);
            //yesButton.SetActive(false);
            //noButton.SetActive(false);
            //acceptButton.SetActive(false);
        }

        if (sentences.Count <= 1 && GameManager.gameManager.favourCompleted == false)
        {
            continueButton.SetActive(false);
            favourButton.SetActive(true);
            //yesButton.SetActive(true);
            //noButton.SetActive(true);
            //acceptButton.SetActive(false);
            //EndDialogue();
            //return;
        }

        if (sentences.Count > 1 && GameManager.gameManager.favourCompleted == true)
        {
            continueButton.SetActive(true);
            //yesButton.SetActive(false);
            //noButton.SetActive(false);
            acceptButton.SetActive(false);
        }

        if (sentences.Count <= 1 && GameManager.gameManager.favourCompleted == true)
        {
            acceptButton.SetActive(true);
            continueButton.SetActive(false);
            //yesButton.SetActive(false);
            //noButton.SetActive(false);
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //yield return null;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        GameManager.gameManager.inDialogue = false;
        GameManager.gameManager.canMove = true;
    }

    public void DeclineFavour()
    {
        CloseFavourWindow();
        EndDialogue();
    }

    /* public void AcceptFavour()
    {
        CloseFavourWindow();
        npc.EnemySpawn();
        GameManager.gameManager.zombiesSpawned = true;
        EndDialogue();
        
    } */

    public void AcceptReward()
    {
        GameManager.gameManager.currentFuel += 150;
        GameManager.gameManager.favourCompleted = false;
        GameManager.gameManager.zombiesSpawned = false;
        EndDialogue();
    }

  

    public void CloseFavourWindow()
    {
        favourAnimator.SetBool("open", false);
    }


    
}
