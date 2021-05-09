using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IshidaShigieDialogueTrigger : DialogueTrigger
{
    public GameObject player;
    public GameObject sasaki;
    public GameObject[] targetlocations;
    public bool reachedEntrance;
    public bool sasakiDone;
    public bool point1;
    public bool duelChoice;
    public bool beggarChoice;
    public bool monkChoice;
    public bool decided;
    public bool alreadyMoved;
    public bool followed;
    public GameObject screenEffect;


    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);

        if(GameDictionary.choiceDictionary["Left Stronghold"])
        {
            this.gameObject.SetActive(false);
        }
        if(GameDictionary.choiceDictionary["Used Hidden Tunnel"])
        {
            this.gameObject.SetActive(false);
        }
        
    }

    void Update()
    {
        //check for conditions for different dialogue options
        if(reachedEntrance)
        {
            if(GameDictionary.choiceDictionary["Samurai Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
            }

            if(GameDictionary.choiceDictionary["Duel Entry"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
            }

            if(GameDictionary.choiceDictionary["Beggar Entry"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
            }

            if(GameDictionary.choiceDictionary["Monk Entry"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
            }
            reachedEntrance = false;
        }

        if(dialogueEnd && !decided)
        {
            if(GameDictionary.choiceDictionary["Samurai Path"] && !alreadyMoved)
            {
                sasaki.GetComponent<SimpleMovementScript>().onSwitch = true;
                sasaki.GetComponent<SasakiKojiroDialogueTriggerSamurai>().shigieDone = true;
                alreadyMoved = true;
            }

            if(GameDictionary.choiceDictionary["Duel Entry"])
            {
                duelChoice = true;
                DecisionDisplay("Duel Ishida Shigie", "Intimidate Ishida Shigie");
            }

            if(GameDictionary.choiceDictionary["Beggar Entry"])
            {
                beggarChoice = true;
                DecisionDisplay("I have a message", "I bring a warning");
            }

            if(GameDictionary.choiceDictionary["Monk Entry"])
            {
                monkChoice = true;
                DecisionDisplay("I have a message", "I bring a warning");
            }
            dialogueEnd = false;
        }

        if(dialogueEnd && decided && !followed)
        {
            GetComponent<NPCFollowPlayerScript>().onSwitch = true;
            followed = true;
        }

        if(sasakiDone)
        {
            // go to location
            GetComponent<ShigieMovementScript>().targetPosition = targetlocations[0];
            GetComponent<ShigieMovementScript>().onSwitch = true;
            sasakiDone = false;
            point1 = true;
        }
        if(point1 && !GetComponent<ShigieMovementScript>().onSwitch)
        {
            GetComponent<ShigieMovementScript>().targetPosition = targetlocations[1];
            GetComponent<ShigieMovementScript>().onSwitch = true;
            point1 = false;
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            player = collider.gameObject;

            if (GameDictionary.choiceDictionary["Passed Shigie"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if (GameDictionary.choiceDictionary["Trained as Samurai"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[9];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if(GameDictionary.choiceDictionary["Shigie Arrested"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[10];
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
        if(duelChoice)
        {
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            screenEffect.SetActive(true);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            decided = true;
        }

        if(beggarChoice)
        {
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            decided = true;
        }

        if(monkChoice)
        {
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            decided = true;
        }
    }

    public override void ButtonB()
    {
        if(duelChoice)
        {
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[8];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            decided = true;
        }

        if(beggarChoice)
        {
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            decided = true;
        }

        if(monkChoice)
        {
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            decided = true;
        }
    }

    private IEnumerator WaitForTime()
    {

        yield return new WaitForSeconds(2);
    }

}
