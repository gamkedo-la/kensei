using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VEGrandDaughterDialogueTrigger : DialogueTrigger
{
    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        GetComponent<NPCFollowPlayerScript>().onSwitch = false;
    }

    void Update()
    {
        //check for conditions for different dialogue options

        //boolean check for if the duel just happened
        if (!GameDictionary.choiceDictionary["Healed Player"] && GameDictionary.choiceDictionary["One Arm"])
        {
            FindObjectOfType<DialogueManager>().forceLock = true;
            GetComponent<NPCFollowPlayerScript>().onSwitch = true;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            GameDictionary.Instance.UpdateEntry("Healed Player", true);
        }
        if (GetComponent<NPCFollowPlayerScript>().stopped == true)
        {
            GetComponent<NPCFollowPlayerScript>().onSwitch = false;
            FindObjectOfType<DialogueManager>().forceLock = false;
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (GameDictionary.choiceDictionary["Healed Player"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            else
            {
                if (GameDictionary.choiceDictionary["Nude"])
                {
                    //pick which Dialogue to run
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                    button.GetComponent<DialogueRun>().trigger = this;
                }
                if (!GameDictionary.choiceDictionary["Nude"])
                {
                    //pick which Dialogue to run
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                    button.GetComponent<DialogueRun>().trigger = this;
                }
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

            if (GameDictionary.choiceDictionary["Healed Player"])
            {
                if (!GetComponent<NPCWander>().onSwitch) GetComponent<NPCWander>().onSwitch = true;
            }
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

    }

    public override void ButtonB()
    {

    }


}
