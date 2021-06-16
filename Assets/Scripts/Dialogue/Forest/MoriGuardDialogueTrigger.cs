using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoriGuardDialogueTrigger : DialogueTrigger
{
    private GameObject player;
    public GameObject screenEffect;
    public GameObject guardTwoL;
    public GameObject guardOneD;
    public GameObject guardTwoD;
    public GameObject doorCollider;
    public bool roninDialogue = false;
    public bool beggarDialogue = false;
    public bool monkDialogue = false;
    public bool samuraiDialogue = false;
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
        if (dialogueEnd && roninDialogue)
        {
            DecisionDisplay("Fight the Guards", "Do Nothing");
            roninDialogue = false;
            dialogueEnd = false;
        }
        if (dialogueEnd && beggarDialogue)
        {
            doorCollider.SetActive(true);
            GameDictionary.Instance.UpdateEntry("Beggar Entry", true);
            beggarDialogue = false;
            dialogueEnd = false;
        }
        if (dialogueEnd && monkDialogue)
        {
            doorCollider.SetActive(true);
            GameDictionary.Instance.UpdateEntry("Monk Entry", true);
            monkDialogue = false;
            dialogueEnd = false;
        }
        if (dialogueEnd && samuraiDialogue)
        {
            GameDictionary.Instance.UpdateEntry("Samurai Path", true);
            GameDictionary.Instance.UpdateEntry("Path Chosen", true);
            doorCollider.SetActive(true);
            samuraiDialogue = false;
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

            //if no path yet
            if (!GameDictionary.choiceDictionary["Path Chosen"])
            {
                if (!GameDictionary.choiceDictionary["One Arm"] && player.GetComponent<PlayerController>().weapon)
                {
                    //run Samurai dialogue
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                    button.GetComponent<DialogueRun>().trigger = this;
                    samuraiDialogue = true;

                }
                else if (GameDictionary.choiceDictionary["One Arm"])
                {
                    //run one arm denial dialogue
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                    button.GetComponent<DialogueRun>().trigger = this;

                }
                else if (!player.GetComponent<PlayerController>().weapon)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                    button.GetComponent<DialogueRun>().trigger = this;
                }
            }
            //else if you're a monk
            else if (GameDictionary.choiceDictionary["Monk Path"])
            {
                if (GameDictionary.choiceDictionary["Monk Robes"])
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                    button.GetComponent<DialogueRun>().trigger = this;
                    monkDialogue = true;
                }
                //run dialogue asking whats up
                else if (GameDictionary.choiceDictionary["One Arm"] && !player.GetComponent<PlayerController>().weapon)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                    button.GetComponent<DialogueRun>().trigger = this;
                    beggarDialogue = true;
                }
                else
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                    button.GetComponent<DialogueRun>().trigger = this;
                }
            }
            //else if you're a ronin
            else if (GameDictionary.choiceDictionary["Ronin Path"])
            {
                //do they pass as a beggar
                if (GameDictionary.choiceDictionary["One Arm"] && !player.GetComponent<PlayerController>().weapon)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                    button.GetComponent<DialogueRun>().trigger = this;
                    beggarDialogue = true;
                }
                else
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                    button.GetComponent<DialogueRun>().trigger = this;
                    roninDialogue = true;
                }
            }

            //else if you're a samurai coming back in
            else if (GameDictionary.choiceDictionary["Samurai Path"])
            {
                button.SetActive(false);
                button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
                button.GetComponent<DialogueRun>().trigger = this;
                doorCollider.SetActive(true);
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
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        screenEffect.SetActive(true);
        guardOneD.SetActive(true);
        guardTwoD.SetActive(true);
        guardTwoL.SetActive(false);
        doorCollider.SetActive(true);
        GameDictionary.Instance.UpdateEntry("Duel Entry", true);
        this.gameObject.SetActive(false);


    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
    }


}
