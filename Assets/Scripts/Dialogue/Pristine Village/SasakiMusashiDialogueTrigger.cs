using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SasakiMusashiDialogueTrigger : DialogueTrigger
{
    bool dialogue1;
    bool dialogue2;
    bool dialogue3;
    bool dialogueOver;
    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    void Update()
    {
        //check for conditions for different dialogue options
        if (dialogue1 && dialogueEnd)
        {
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            dialogue1 = false;
            dialogue2 = true;
        }
        if (dialogue2 && dialogueEnd)
        {
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            dialogue2 = false;
            dialogue3 = true;
        }
        if (dialogue3 && dialogueEnd)
        {
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            dialogue3 = false;
            dialogueOver = true;
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            if (!dialogueOver)
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
                dialogue1 = true;
            }

        }
        if (dialogueOver)
        {
            button.SetActive(false);
        }
    }

    public override void OnTriggerExit2D(Collider2D collider)
    {
        inRange = false;
        button.SetActive(false);
        button.GetComponent<DialogueRun>().dialogue = null;
        button.GetComponent<DialogueRun>().trigger = null;
        dialogueEnd = false;
        panel.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    public override void DecisionDisplay(string buttonAText, string buttonBText)
    {
        this.buttonA.GetComponentInChildren<Text>().text = buttonAText;
        this.buttonB.GetComponentInChildren<Text>().text = buttonBText;
        this.buttonA.SetActive(true);
        this.buttonB.SetActive(true);
    }

    public override void ButtonA()
    {

    }

    public override void ButtonB()
    {

    }


}
