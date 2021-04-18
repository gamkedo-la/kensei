using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SasakiKojiroDialogueTrigger : DialogueTrigger
{
    public GameObject player;
    public GameObject screen;

    public GameObject villageMessenger;

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

        if (dialogueEnd && !GameDictionary.choiceDictionary["Path Chosen"])
        {
            DecisionDisplay("Train as Ronin", "Pass for Now");
            dialogueEnd = false;
        }

    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            player = collider.gameObject;

            if (!GameDictionary.choiceDictionary["Path Chosen"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
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
        screen.SetActive(true);
        GameDictionary.Instance.UpdateEntry("Ronin Path", true);
        GameDictionary.Instance.UpdateEntry("Path Chosen", true);
        StartCoroutine(TimePasses());

        button.SetActive(false);
        button.GetComponent<DialogueRun>().dialogue = null;
        button.GetComponent<DialogueRun>().trigger = null;
        dialogueEnd = false;
        panel.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        //GetComponent<SimpleMovementScript>().onSwitch = true;

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

    private IEnumerator TimePasses()
    {
        yield return new WaitForSeconds(2);
        screen.SetActive(false);
        villageMessenger.SetActive(true);
        villageMessenger.GetComponent<SimpleMovementScript>().onSwitch = true;
    }

}
