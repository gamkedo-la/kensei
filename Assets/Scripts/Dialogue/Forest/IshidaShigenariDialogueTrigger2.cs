using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IshidaShigenariDialogueTrigger2 : DialogueTrigger
{
    public GameObject player;
    public GameObject shigenariBlocker;
    public bool takuanDialogue = false;
    public bool duelDialogue = false;
    bool dueled;
    public GameObject screenEffect;
    public GameObject arm;
    public GameObject deadShigenari;
    int switchInt;
    public GameObject takuan;

    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);

        if (GameDictionary.choiceDictionary["Samurai Path"])
        {
            this.gameObject.SetActive(true);
            shigenariBlocker.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            shigenariBlocker.SetActive(false);
        }

    }

    void Update()
    {
        //check for conditions for different dialogue options
        if (dialogueEnd && takuanDialogue)
        {
            switchInt = 0;
            DecisionDisplay("I've Heard Enough", "You've Made Your Point");
            takuanDialogue = false;
            dialogueEnd = false;
        }
        if (dialogueEnd && duelDialogue && !dueled)
        {
            switchInt = 1;
            DecisionDisplay("Fight the Duel", "Back Off");
            takuanDialogue = false;
            dialogueEnd = false;
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            player = collider.gameObject;

            if (GameDictionary.choiceDictionary["With Takuan"])
            {
                takuan.GetComponent<NPCFollowPlayerScript>().onSwitch = false;
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                takuanDialogue = true;
            }
            else if (!GameDictionary.choiceDictionary["With Takuan"] && !dueled)
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
                duelDialogue = true;
            }
            else if (!GameDictionary.choiceDictionary["With Takuan"] && dueled)
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
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
        switch (switchInt)
        {
            case 0:
                break;

            case 1:
                screenEffect.SetActive(true);
                if (player.GetComponent<StateTracker>().combatScore > 20)
                {
                    deadShigenari.SetActive(true);
                    GameDictionary.Instance.UpdateEntry("Shigenari Dead", true);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    arm.SetActive(true);
                    GameDictionary.Instance.UpdateEntry("One Arm", true);
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                    button.GetComponent<DialogueRun>().trigger = this;
                    player.GetComponent<PlayerController>().ChooseAnimator();
                    dueled = true;
                }
                break;
        }
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);

    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);

    }


}
