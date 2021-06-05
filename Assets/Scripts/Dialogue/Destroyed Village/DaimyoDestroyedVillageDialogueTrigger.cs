using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaimyoDestroyedVillageDialogueTrigger : DialogueTrigger
{
    bool shigeieDialogueTime;
    bool shigeieDialogueEnd;
    public GameObject shigeie;
    public GameObject sasaki;
    public GameObject screenEffect;
    bool shigeieApprehended;
    bool shigeieAttacks;
    bool interactionOver;
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
        if (dialogueEnd && shigeieDialogueTime)
        {
            dialogueEnd = false;
            shigeieDialogueTime = false;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            shigeie.GetComponent<SimpleMovementScript>().onSwitch = true;
            shigeieDialogueEnd = true;
        }
        if (dialogueEnd && shigeieDialogueEnd)
        {
            shigeieAttacks = true;
            if (GameDictionary.choiceDictionary["Samurai Path"] || GameDictionary.choiceDictionary["Ronin Path"])
            {
                dialogueEnd = false;
                shigeieDialogueEnd = false;
                DecisionDisplay("Save the Daimyo", "Do Nothing");
            }
            else
            {
                dialogueEnd = false;
                shigeieDialogueEnd = false;
                sasaki.GetComponent<SimpleMovementScript>().onSwitch = true;
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
                shigeieApprehended = true;
            }
        }
        if (dialogueEnd && shigeieApprehended && shigeieAttacks)
        {
            dialogueEnd = false;
            interactionOver = true;
            shigeieAttacks = false;
        }
        else if (dialogueEnd && !shigeieApprehended && shigeieAttacks)
        {   
            dialogueEnd = true;
            interactionOver = true;
            shigeieAttacks = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (GameDictionary.choiceDictionary["Shigenari Dead"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                shigeieDialogueTime = true;
            }
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
