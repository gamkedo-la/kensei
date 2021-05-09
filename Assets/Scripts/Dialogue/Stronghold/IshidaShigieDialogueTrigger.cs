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

            if(GameDictionary.choiceDictionary["Ronin Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
            }

            if(GameDictionary.choiceDictionary["Monk Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
            }
            reachedEntrance = false;
        }

        if(dialogueEnd)
        {
            if(GameDictionary.choiceDictionary["Samurai Path"])
            {
                sasaki.GetComponent<SimpleMovementScript>().onSwitch = true;
                sasaki.GetComponent<SasakiKojiroDialogueTrigger2>().shigieDone = true;
            }
            dialogueEnd = false;
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
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
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
        
    }

    public override void ButtonB()
    {

    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(2);
    }

}
