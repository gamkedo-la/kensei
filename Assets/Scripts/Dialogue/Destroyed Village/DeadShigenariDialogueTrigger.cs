using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadShigenariDialogueTrigger : DialogueTrigger
{
    public GameObject shigeie;
    public GameObject sakichi;
    public GameObject shigeie1;
    public GameObject sakichi1;

    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);

        if (GameDictionary.choiceDictionary["Shigenari Dead"])
        {
            this.gameObject.SetActive(false);
            shigeie.SetActive(false);
            sakichi.SetActive(false);
        }
        else
        {
            shigeie1.SetActive(false);
            sakichi1.SetActive(false);
        }
    }

    void Update()
    {
        //check for conditions for different dialogue options
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;

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