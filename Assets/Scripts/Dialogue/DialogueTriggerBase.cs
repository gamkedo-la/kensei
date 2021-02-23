using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerBase : DialogueTrigger
{

    public void Start()
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

    public void OnTriggerEnter2D(Collider2D collider)
    {
        inRange = true;
        button.SetActive(true);

            if(/*some condition*/true)
            {
            //pick which Dialogue to run
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            }   
    }
    
    public void OnTriggerExit2D(Collider2D collider)
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

    public void DecisionDisplay(string buttonAText, string buttonBText)
    {
        this.buttonA.GetComponentInChildren<Text>().text = buttonAText;
        this.buttonB.GetComponentInChildren<Text>().text = buttonBText;
        this.buttonA.SetActive(true);
        this.buttonB.SetActive(true);
    }


}
