using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakuanSohoDialogueTrigger : DialogueTrigger
{
    public int switchInt;
    public GameObject player;
    public GameObject sakichi;
    public GameObject sasaki;
    public GameObject shigenari;
    public GameObject monksRobes; 
    public GameObject screen;
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
      if(dialogueEnd && !GameDictionary.choiceDictionary["Path Chosen"])
      {
          switchInt = 1;
          DecisionDisplay("Join the Monks", "Pass for Now");
          dialogueEnd = false;
      }

      if(GameDictionary.choiceDictionary["Monk Path"] && !GameDictionary.choiceDictionary["Spoke to Sakichi"] && dialogueEnd)
      {
          dialogueEnd = false;
          sakichi.SetActive(true);
      }

      if(GameDictionary.choiceDictionary["Spoke to Sakichi"] && !GameDictionary.choiceDictionary["Spoke to Takuan"] && dialogueEnd)
      {
            switchInt = 2;
            DecisionDisplay("Ishida Shigie is planning an attack", "Ishida Sakichi needs our help");
            dialogueEnd = false;
      }
      if(GameDictionary.choiceDictionary["Samurai Path"] && dialogueEnd)
      {
          GetComponent<NPCFollowPlayerScript>().onSwitch = true;
      }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
        inRange = true;
        button.SetActive(true);
        player = collider.gameObject;

            if(!GameDictionary.choiceDictionary["Path Chosen"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if(GameDictionary.choiceDictionary["Monk Path"] && !GameDictionary.choiceDictionary["Spoke to Sakichi"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if(GameDictionary.choiceDictionary["Monk Path"] && GameDictionary.choiceDictionary["Spoke to Sakichi"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if(GameDictionary.choiceDictionary["Spoke to Takuan"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                button.GetComponent<DialogueRun>().trigger = this;
            }
            if(GameDictionary.choiceDictionary["Samurai Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
                button.GetComponent<DialogueRun>().trigger = this;
            }
        }   
    }
    
    public override void OnTriggerExit2D(Collider2D collider)
    {
        inRange = false;
        player = collider.gameObject;
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
        switch(switchInt)
        {
            case 1:
                screen.SetActive(true);
                player.GetComponent<PlayerController>().AddItem(monksRobes);
                GameDictionary.Instance.UpdateEntry("Monk Robes", true);
                GameDictionary.Instance.UpdateEntry("Monk Path", true);
                GameDictionary.Instance.UpdateEntry("Path Chosen", true);
                sasaki.SetActive(false);
                shigenari.SetActive(false);
                StartCoroutine(TimePasses());

                button.SetActive(false);
                button.GetComponent<DialogueRun>().dialogue = null;
                button.GetComponent<DialogueRun>().trigger = null;
                dialogueEnd = false;
                panel.SetActive(false);
                buttonA.SetActive(false);
                buttonB.SetActive(false);
                combatScore.SetActive(false);
                break;
            
            case 2:
                sakichi.SetActive(false);
                buttonA.SetActive(false);
                buttonB.SetActive(false);
                combatScore.SetActive(false);
                GameDictionary.Instance.UpdateEntry("Spoke to Takuan", true);
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
                break;
        }
    }

    public override void ButtonB()
    {
        switch(switchInt)
        {
            case 1:
                inRange = false;
                button.SetActive(false);
                button.GetComponent<DialogueRun>().dialogue = null;
                button.GetComponent<DialogueRun>().trigger = null;
                dialogueEnd = false;
                panel.SetActive(false);
                buttonA.SetActive(false);
                buttonB.SetActive(false);
                combatScore.SetActive(false);
                break;
            
            case 2:
                sakichi.SetActive(false);
                buttonA.SetActive(false);
                buttonB.SetActive(false);
                combatScore.SetActive(false);
                GameDictionary.Instance.UpdateEntry("Spoke to Takuan", true);
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
                button.GetComponent<DialogueRun>().TriggerDialogue();
                break;
        }
        
    }

    private IEnumerator TimePasses()
    {
        yield return new WaitForSeconds(2);
        screen.SetActive(false);
    }


}
