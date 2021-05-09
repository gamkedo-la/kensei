using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SasakiKojiroDialogueTrigger2 : DialogueTrigger
{
    public GameObject player;
    public GameObject shigie;
    public GameObject triggerCollider;
    public bool shigieDone;

    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    void Update()
    {
        if (shigieDone)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            shigieDone = false;
        }

        if (dialogueEnd)
        {
            GetComponent<NPCFollowPlayerScript>().onSwitch = true;
            GameDictionary.Instance.UpdateEntry("Passed Shigie", true);
            shigie.GetComponent<IshidaShigieDialogueTrigger>().sasakiDone = true;
            FindObjectOfType<PlayerController>().movementLocked = false;
            inRange = false;
            button.SetActive(false);
            button.GetComponent<DialogueRun>().dialogue = null;
            button.GetComponent<DialogueRun>().trigger = null;
            dialogueEnd = false;
            panel.SetActive(false);
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            combatScore.SetActive(false);
            triggerCollider.SetActive(false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (/*some condition*/true)
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
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


}
