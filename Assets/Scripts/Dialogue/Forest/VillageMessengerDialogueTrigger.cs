using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageMessengerDialogueTrigger : DialogueTrigger
{
    bool spoke = false;
    bool spokeAgain = false;
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
        if (GameDictionary.choiceDictionary["Ronin Path"] && !spoke && !GetComponent<SimpleMovementScript>().onSwitch)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            spoke = true;
        }
        if (dialogueEnd && spoke && !spokeAgain)
        {
            GameDictionary.Instance.UpdateEntry("Message Delivered", true);
            dialogueEnd = false;
        }

        if (GameDictionary.choiceDictionary["Sasaki Returned"] && !spokeAgain)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            spokeAgain = true;
        }

        if (spokeAgain && dialogueEnd && !(GameDictionary.choiceDictionary["Player Leads"] || GameDictionary.choiceDictionary["Shigenari Leads"]))
        {
            Debug.Log("Made it to decision");
            DecisionDisplay("Lead the Villagers", "Let Shigenari Lead");
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (/*some condition*/true)
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
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
        button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
        GameDictionary.Instance.UpdateEntry("Player Leads", true);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
    }

    public override void ButtonB()
    {
        button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
        GameDictionary.Instance.UpdateEntry("Shigenari Leads", true);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
    }


}
