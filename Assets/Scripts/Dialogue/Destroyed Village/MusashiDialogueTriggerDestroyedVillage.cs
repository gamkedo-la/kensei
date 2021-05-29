using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusashiDialogueTriggerDestroyedVillage : DialogueTrigger
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
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
        inRange = true;
        button.SetActive(true);

            if(GameDictionary.choiceDictionary["Samurai Path"] && GameDictionary.choiceDictionary["Dueled Musashi Destroyed"])
            {
            //pick which Dialogue to run
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            }
            if(GameDictionary.choiceDictionary["Samurai Path"] && !GameDictionary.choiceDictionary["Dueled Musashi Destroyed"])
            {
            //pick which Dialogue to run
            button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
            button.GetComponent<DialogueRun>().trigger = this;
            }
            if(GameDictionary.choiceDictionary["Ronin Path"])
            {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
            button.GetComponent<DialogueRun>().trigger = this;
            }
            if(GameDictionary.choiceDictionary["Monk Path"])
            {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
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

    }

    public override void ButtonB()
    {

    }


}
