using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IshidaSakichiDialogueTrigger : DialogueTrigger
{

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
        if (!GameDictionary.choiceDictionary["Spoke to Sakichi"] && dialogueEnd)
        {
            dialogueEnd = false;
            DecisionDisplay("Leave it to me", "I'll think about it");
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (!GameDictionary.choiceDictionary["Spoke to Sakichi"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if (GameDictionary.choiceDictionary["Spoke to Sakichi"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
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
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        GameDictionary.Instance.UpdateEntry("Spoke to Sakichi", true);

        button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
    }

    public override void ButtonB()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        GameDictionary.Instance.UpdateEntry("Spoke to Sakichi", true);

        button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
    }


}
