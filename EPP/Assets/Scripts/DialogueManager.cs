using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    [Space]


    [Space]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [Space]
    public Animator animator;
    [Space]
    public GameObject dialogueBox;
    public GameObject favourScreen;
    //public GameObject yesButton;
    //public GameObject noButton;
    public GameObject continueButton;
    public GameObject acceptButton;
    public GameObject favourButton;
    [Space]
    public NPC _npc;
    [Space]
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _npc.deniedFavour = false;
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
        if (sentences.Count > 1 && gameManager.favourCompleted == false)
        {
            continueButton.SetActive(true);
            //yesButton.SetActive(false);
            //noButton.SetActive(false);
            //acceptButton.SetActive(false);
        }

        if (sentences.Count <= 1 && gameManager.favourCompleted == false)
        {
            continueButton.SetActive(false);
            favourButton.SetActive(true);
            //yesButton.SetActive(true);
            //noButton.SetActive(true);
            //acceptButton.SetActive(false);
            //EndDialogue();
            //return;
        }

        if (sentences.Count > 1 && gameManager.favourCompleted == true)
        {
            continueButton.SetActive(true);
            //yesButton.SetActive(false);
            //noButton.SetActive(false);
            acceptButton.SetActive(false);
        }

        if (sentences.Count <= 1 && gameManager.favourCompleted == true)
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
        gameManager.inDialogue = false;
        gameManager.canMove = true;
    }

    public void DeclineFavour()
    {
        _npc.CloseFavourWindow();
        _npc.deniedFavour = true;
        EndDialogue();
    }

     public void AcceptFavour()
    {
        _npc.CloseFavourWindow();
        _npc.EnemySpawn();
        gameManager.zombiesSpawned = true;
        EndDialogue();
        
    }

    public void Favour()
    {
        _npc.OpenFavourWindow();
    }

    public void AcceptReward()
    {
        gameManager.currentFuel += 150;
        _npc.deniedFavour = false;
        gameManager.favourCompleted = false;
        gameManager.zombiesSpawned = false;
        EndDialogue();
    }


    
}
