using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerMusashi : DialogueTrigger
{
public GameObject arm;
public bool Dialogue1 = false;
    public override void Start()
    {
       button.SetActive(false); 
       buttonA.SetActive(false);
       buttonB.SetActive(false);
       combatScore.SetActive(false);
    }

    void Update()
    {
        if(dialogueEnd && Dialogue1)
        {
            DecisionDisplay("Challenge", "Walk Away");
            dialogueEnd = false;
            Dialogue1 = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        inRange = true;
        
        button.SetActive(true);

        if(!GameDictionary.choiceDictionary["Nude"])
        {
            if(!GameDictionary.choiceDictionary["One Arm"])   
            { 
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            Dialogue1 = true;

            }
            else
            {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
            button.GetComponent<DialogueRun>().trigger = this;
            }
        }
        else
        {
            if(!GameDictionary.choiceDictionary["One Arm"])
            {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
            button.GetComponent<DialogueRun>().trigger = this;

            }
            else
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
        Dialogue1 = false;
        panel.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    public override void DecisionDisplay(string buttonAText, string buttonBText)
    {
        buttonA.GetComponentInChildren<Text>().text = buttonAText;
        buttonB.GetComponentInChildren<Text>().text = buttonBText;
        buttonA.SetActive(true);
        buttonB.SetActive(true);
        combatScore.SetActive(true);
    }
    public override void ButtonA()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        GameDictionary.choiceDictionary["One Arm"] = true;
        arm.SetActive(true);
    }
    public override void ButtonB()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

}
