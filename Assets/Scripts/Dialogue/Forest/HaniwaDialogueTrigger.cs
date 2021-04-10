using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaniwaDialogueTrigger : DialogueTrigger
{
    public GameObject player;

    public GameObject brokenHaniwaStand;
    public GameObject fixedHaniwaStand; 

    public GameObject chokuto;

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
      if (dialogueEnd && GameDictionary.choiceDictionary["Broken Haniwa"])
      {
          DecisionDisplay("Repair the Haniwa", "Do nothing");
          dialogueEnd = false;
      }

      if(dialogueEnd && !GameDictionary.choiceDictionary["Broken Haniwa"])
      {
          GiveChokuto();
          dialogueEnd = false;
      }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
        inRange = true;
        player = collider.gameObject;

            if(GameDictionary.choiceDictionary["Broken Haniwa"])
            {
            //pick which Dialogue to run
            button.SetActive(true);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            }
        }   
    }
    
    public override void OnTriggerExit2D(Collider2D collider)
    {
        player = collider.gameObject;
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
        player.GetComponent<PlayerController>().smallItem = null;
        GameDictionary.Instance.UpdateEntry("Broken Haniwa", false);
        brokenHaniwaStand.SetActive(false);
        fixedHaniwaStand.SetActive(true);
        dialogueEnd = false;

        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
    
    }

    public override void ButtonB()
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
    public void GiveChokuto()
    {
        player.GetComponent<PlayerController>().AddItem(chokuto);
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
