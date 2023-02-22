using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerReturn : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerReturnDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
