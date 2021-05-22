using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveDialogueTrigger : DialogueTrigger
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
        }
    }
}
