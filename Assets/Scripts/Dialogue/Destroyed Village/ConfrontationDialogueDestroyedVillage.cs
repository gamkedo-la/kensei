using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfrontationDialogueDestroyedVillage : DialogueTrigger
{
    bool initialDialogue;
    bool secondDialogue;
    bool thirdDialogue;
    bool duelDialogue;
    bool noMoreDialogue;
    public GameObject armScene;

    public GameObject screenEffect;
    public GameObject player;
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
        if (initialDialogue && dialogueEnd)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
            button.GetComponent<DialogueRun>().trigger = this;
            initialDialogue = false;
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            secondDialogue = true;
        }
        if (secondDialogue && dialogueEnd)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
            button.GetComponent<DialogueRun>().trigger = this;
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            secondDialogue = false;
            thirdDialogue = true;
        }
        if (thirdDialogue && dialogueEnd && !GameDictionary.choiceDictionary["One Arm"] && GameDictionary.choiceDictionary["Samurai Path"])
        {
            DecisionDisplay("Challenge Musashi", "Do Nothing");
        }
        if (thirdDialogue && dialogueEnd && GameDictionary.choiceDictionary["One Arm"] && GameDictionary.choiceDictionary["Samurai Path"])
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
            button.GetComponent<DialogueRun>().trigger = this;
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            thirdDialogue = false;
        }
        if (thirdDialogue && dialogueEnd && !GameDictionary.choiceDictionary["Samurai Path"])
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
            button.GetComponent<DialogueRun>().trigger = this;
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            thirdDialogue = false;
            noMoreDialogue = true;
        }
        if(duelDialogue && dialogueEnd)
        {
            screenEffect.SetActive(true);
            armScene.SetActive(true);
            GameDictionary.Instance.UpdateEntry("One Arm", true);
            player.GetComponent<PlayerController>().ChooseAnimator();
            GameDictionary.Instance.UpdateEntry("Dueled Musashi Destroyed", true);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
            button.GetComponent<DialogueRun>().trigger = this;
            dialogueEnd = false;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            duelDialogue = false;
            noMoreDialogue = true;
        }
        if(noMoreDialogue && dialogueEnd)
        {
            this.gameObject.SetActive(false);
            player.GetComponent<PlayerController>().movementLocked = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            player = collider.gameObject;
            //pick which Dialogue to run
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            initialDialogue = true;
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
        player.GetComponent<PlayerController>().movementLocked = false;
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
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        thirdDialogue = false;
        duelDialogue = true;
        button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        thirdDialogue = false;
        button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
        noMoreDialogue = true;
    }
}
