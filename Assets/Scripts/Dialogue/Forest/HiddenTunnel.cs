using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenTunnel : DialogueTrigger
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

            if(GameDictionary.choiceDictionary["Rusted Key"])
            {
                DecisionDisplay("Use the Rusty Key", "Do Nothing");
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
        //load in daimyo stronghold
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
    }

    public override void ButtonB()
    {
        //do nothing...
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
    }


}
