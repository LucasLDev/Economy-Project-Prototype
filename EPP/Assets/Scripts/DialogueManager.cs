using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    [Space]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [Space]
    public Animator animator;
    [Space]
    public GameObject dialogueBox;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject continueButton;
    [Space]
    public NPC _npc;
    [Space]
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen",true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
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
        if (sentences.Count < 1)
        {
            continueButton.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
        }

        if (sentences.Count == 1)
        {
            continueButton.SetActive(false);
            yesButton.SetActive(true);
            noButton.SetActive(true);
            //EndDialogue();
            //return;
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
        gameManager.canMove = true;
    }

    public void DeclineFavour()
    {
        
        EndDialogue();
        return;
        
    }

     public void AcceptFavour()
    {
        _npc.EnemySpawn();
        gameManager.zombiesSpawned = true;
        EndDialogue();
        return;
    }


    
}
