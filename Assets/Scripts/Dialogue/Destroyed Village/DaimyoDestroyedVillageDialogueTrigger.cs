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
    public GameObject shigeie1;
    public GameObject sasaki1;
    public GameObject screenEffect;
    public GameObject deadDaimyo;
    public GameObject armScene;
    public GameObject player;
    bool shigeieApprehended;
    bool shigeieAttacks;
    bool interactionOver;
    bool skipDialogue;
    bool dialogueOver;
    public Sprite shigeie1A;
    int switchInt = 0;
    bool endGame;
    public GameObject endGamePanel;

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
        if (!GameDictionary.choiceDictionary["Shigeie Apprehended"] || !GameDictionary.choiceDictionary["Last Scene Over"])
        {

            switchInt = 0;
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
                    GameDictionary.Instance.UpdateEntry("Shigeie Triggered", true);
                    shigeieDialogueEnd = true;
                }
                if (dialogueEnd && shigeieDialogueEnd)
                {
                    if (GameDictionary.choiceDictionary["Samurai Path"] || GameDictionary.choiceDictionary["Ronin Path"])
                    {
                        dialogueEnd = false;
                        shigeieDialogueEnd = false;
                        shigeie.GetComponent<EndgameSimpleMovementScript>().onSwitch = true;
                    }
                    else
                    {
                        shigeie.GetComponent<EndgameSimpleMovementScript>().onSwitch = true;
                        dialogueEnd = false;
                        shigeieDialogueEnd = false;
                        shigeieAttacks = true;
                        //Sasaki says something
                        button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                        button.GetComponent<DialogueRun>().trigger = this;
                        // button.GetComponent<DialogueRun>().TriggerDialogue();
                        //shigeieApprehended = true;
                    }
                }
                if (dialogueEnd && shigeieApprehended && shigeieAttacks)
                {
                    dialogueEnd = false;
                    interactionOver = true;
                    shigeieAttacks = false;
                    GameDictionary.Instance.UpdateEntry("Shigeie Apprehended", true);
                    shigeie.GetComponent<Animator>().enabled = false;
                    shigeie.GetComponent<SpriteRenderer>().sprite = shigeie1A;
                    armScene.SetActive(true);
                    screenEffect.SetActive(true);
                    screenEffect.GetComponent<ScreenEffect>().StartCoroutine("WaitForAnim");
                    //shigeie says something like ouch
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
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
                    else
                    {
                        dialogueEnd = false;
                        shigeieDialogueEnd = false;
                        //shigeieAttacks = false;
                        //Sasaki says something
                        sasaki.GetComponent<EndgameSasakiSimpleMovementScript>().onSwitch = true;
                        shigeieApprehended = true;
                    }
                }
                if (dialogueEnd && interactionOver && !shigeieApprehended && GameDictionary.choiceDictionary["Opted Let Die"] && !endGame)
                {
                    endGame = true;
                    endGamePanel.SetActive(true);
                }
            }
            else if (skipDialogue)
            {
                if (dialogueEnd && !interactionOver && !dialogueOver)
                {
                    dialogueEnd = false;
                    interactionOver = true;
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }
                else if (dialogueEnd && interactionOver && !dialogueOver)
                {
                    dialogueEnd = false;
                    interactionOver = false;
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[8];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                    dialogueOver = true;
                    GameDictionary.Instance.UpdateEntry("Last Scene Over", true);
                }
            }
        }
        else if (dialogueEnd && (GameDictionary.choiceDictionary["Shigeie Apprehended"] || GameDictionary.choiceDictionary["Last Scene Over"]))
        {
            switchInt = 1;
            DecisionDisplay("End the Game", "Keep Exploring");
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            player = collider.gameObject;

            if (!GameDictionary.choiceDictionary["Shigenari Dead"])
            {
                //pick which Dialogue to run
                //the daimyo says something
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                shigeieDialogueTime = true;
                skipDialogue = false;
            }
            else if (GameDictionary.choiceDictionary["Shigenari Dead"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
                button.GetComponent<DialogueRun>().trigger = this;
                skipDialogue = true;

            }
            if (GameDictionary.choiceDictionary["Shigeie Apprehended"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
                button.GetComponent<DialogueRun>().trigger = this;
            }
            if (GameDictionary.choiceDictionary["Last Scene Over"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[9];
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
        switch (switchInt)
        {

            case 0:
                //set up switch to end game
                this.buttonA.SetActive(false);
                this.buttonB.SetActive(false);
                shigeieAttacks = true;
                shigeieApprehended = true;
                GameDictionary.Instance.UpdateEntry("Opted Save", true);
                dialogueEnd = true;
                break;

            case 1:
            SceneLoader.Load("Epilogue");
                break;
        }
    }

    public override void ButtonB()
    {
        //set up switch to end game
        switch (switchInt)
        {

            case 0:
                this.buttonA.SetActive(false);
                this.buttonB.SetActive(false);
                shigeieAttacks = true;
                if (GameDictionary.choiceDictionary["Ronin Path"])
                {
                    shigeieApprehended = false;
                    GameDictionary.Instance.UpdateEntry("Opted Let Die", true);
                    dialogueEnd = true;
                }
                if (GameDictionary.choiceDictionary["Samurai Path"])
                {
                    //Sasaki says something
                    shigeieApprehended = true;
                    GameDictionary.Instance.UpdateEntry("Opted Let Die", true);
                    dialogueEnd = true;
                    sasaki.GetComponent<EndgameSasakiSimpleMovementScript>().onSwitch = true;
                }
                break;

            case 1:
                break;
        }

    }

    public void MoveSasaki()
    {
        sasaki.GetComponent<SimpleMovementScript>().onSwitch = true;
    }
}
