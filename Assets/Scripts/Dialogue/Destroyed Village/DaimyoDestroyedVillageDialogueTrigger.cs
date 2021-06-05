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
    public GameObject deadDaimyo;
    public GameObject armScene;
    bool shigeieApprehended;
    bool shigeieAttacks;
    bool interactionOver;
    bool skipDialogue;
    public Sprite shigeie1A;

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
        if (!skipDialogue)
        {
            if (dialogueEnd && shigeieDialogueTime)
            {
                dialogueEnd = false;
                shigeieDialogueTime = false;
                //shigeii says something
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
                    //Sasaki says something
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
                screenEffect.SetActive(true);
                GameDictionary.Instance.UpdateEntry("Shigeie Apprehended", true);
                shigeie.GetComponent<SpriteRenderer>().sprite = shigeie1A;
                //shigeie says something like ouch
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
                armScene.SetActive(true);
            }
            else if (dialogueEnd && !shigeieApprehended && shigeieAttacks)
            {
                if (GameDictionary.choiceDictionary["Ronin Path"])
                {
                    dialogueEnd = false;
                    interactionOver = true;
                    shigeieAttacks = false;
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    //shigeie says something about kill Daimyo
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                    deadDaimyo.SetActive(true);
                }
                if (GameDictionary.choiceDictionary["Samurai Path"])
                {
                    dialogueEnd = false;
                    shigeieDialogueEnd = false;
                    sasaki.GetComponent<SimpleMovementScript>().onSwitch = true;
                    //Sasaki says something
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                    shigeieApprehended = true;
                }
            }
            else if (dialogueEnd && interactionOver && !shigeieApprehended && GameDictionary.choiceDictionary["Opted Let Die"])
            {
                // end game
            }
        }
        else if (skipDialogue && dialogueEnd && !interactionOver)
        {
            dialogueEnd = false;
            interactionOver = true;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
        }
        else if (skipDialogue && dialogueEnd && interactionOver)
        {
            dialogueEnd = false;
            interactionOver = true;
            button.GetComponent<DialogueRun>().dialogue = Dialogues[8];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
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
                //the daimyo says something
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                shigeieDialogueTime = true;
            }
            if (!GameDictionary.choiceDictionary["Shigenari Dead"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
                button.GetComponent<DialogueRun>().trigger = this;
                skipDialogue = true;
            }
            if (dialogueEnd && interactionOver && GameDictionary.choiceDictionary["Shigeie Apprehended"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
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
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        interactionOver = true;
        dialogueEnd = true;
        shigeieApprehended = true;
        GameDictionary.Instance.UpdateEntry("Opted Save", true);
    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        if (GameDictionary.choiceDictionary["Ronin Path"])
        {
            dialogueEnd = true;
            interactionOver = true;
            shigeieApprehended = false;
            GameDictionary.Instance.UpdateEntry("Opted Let Die", true);
        }
        if (GameDictionary.choiceDictionary["Samurai Path"])
        {
            dialogueEnd = true;
            interactionOver = true;
            shigeieApprehended = true;
            GameDictionary.Instance.UpdateEntry("Opted Let Die", true);
            GameDictionary.Instance.UpdateEntry("Opted Let Die", true);
        }

    }


}
