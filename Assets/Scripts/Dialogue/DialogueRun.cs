using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRun : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueTrigger trigger;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().trigger = trigger;
    }
}
