using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IshidaSakichiDialogueTriggerPV : DialogueTrigger
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

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
        inRange = true;
        button.SetActive(true);
            if(GameDictionary.choiceDictionary["Shigie Arrested"])
            {

                if(GameDictionary.choiceDictionary["Samurai Path"] && GameDictionary.choiceDictionary["Shigenari Dead"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                }

                else if(GameDictionary.choiceDictionary["Samurai Path"] && !GameDictionary.choiceDictionary["Shigenari Dead"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
                }

                if(GameDictionary.choiceDictionary["Ronin Path"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
                }

                if(GameDictionary.choiceDictionary["Monk Path"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
                }
            }
            if(GameDictionary.choiceDictionary["Sake with Daimyo"])
            {
                if(GameDictionary.choiceDictionary["Samurai Path"] && GameDictionary.choiceDictionary["Shigenari Dead"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                button.GetComponent<DialogueRun>().trigger = this;
                }

                else if(GameDictionary.choiceDictionary["Samurai Path"] && !GameDictionary.choiceDictionary["Shigenari Dead"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
                button.GetComponent<DialogueRun>().trigger = this;
                }

                if(GameDictionary.choiceDictionary["Ronin Path"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
                button.GetComponent<DialogueRun>().trigger = this;
                }

                if(GameDictionary.choiceDictionary["Monk Path"])
                {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
                button.GetComponent<DialogueRun>().trigger = this;
                }
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