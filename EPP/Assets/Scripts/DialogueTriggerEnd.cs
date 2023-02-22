using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerEnd : MonoBehaviour
{
   public Dialogue dialogue;

    public void TriggerEndDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
