using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SasakiKojiroDialogueTriggerSamurai : DialogueTrigger
{
    public GameObject player;
    public GameObject shigie;
    public GameObject triggerCollider;
    public GameObject timePasses;
    public bool shigieDone;
    public bool timePassed;
    public bool interrupted;
    public bool spokeToDaimyo;

    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);

        if(GameDictionary.choiceDictionary["Left Stronghold"])
        {
            this.gameObject.SetActive(false);
        }
        
        if(GameDictionary.choiceDictionary["Used Hidden Tunnel"])
        {
            this.gameObject.SetActive(false);
        }

    }

    void Update()
    {
        if (GameDictionary.choiceDictionary["Monk Path"] || GameDictionary.choiceDictionary["Ronin Path"])
        {
            this.gameObject.SetActive(false);
        }

        if (shigieDone && !GameDictionary.choiceDictionary["Daimyo Service"] && !interrupted)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
            shigieDone = false;
        }

        if (dialogueEnd && !GameDictionary.choiceDictionary["Daimyo Service"])
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
            GetComponent<SimpleMovementScript>().onSwitch = false;
            interrupted = true;
        }

        if (dialogueEnd && spokeToDaimyo && !timePassed)
        {
            dialogueEnd = false;
            GameDictionary.Instance.UpdateEntry("Trained as Samurai", true);
            StartCoroutine(WaitForTime());
            timePassed = true;
            player = FindObjectOfType<PlayerController>().gameObject;
            player.GetComponent<StateTracker>().playerCombatPoints += 10;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (GameDictionary.choiceDictionary["Daimyo Service"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
                spokeToDaimyo = true;
            }
            if (GameDictionary.choiceDictionary["Trained as Samurai"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
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

    private IEnumerator WaitForTime()
    {
        timePasses.SetActive(true);
        yield return new WaitForSeconds(2);
        timePasses.SetActive(false);
    }
}
