using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShinzoShrineDialogueTrigger : DialogueTrigger
{
    public GameObject player;
    public GameObject charm;
    public GameObject completeShrine;


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
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            player = collider.gameObject;

            if (GameDictionary.choiceDictionary["Shinzo Fragment"])
            {
                button.GetComponent<DialogueRun>().trigger = this;
                DecisionDisplay("Return the Shinzo Fragment", "Do Nothing");
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
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        player.GetComponent<PlayerController>().bigItem = null;
        player.GetComponent<PlayerController>().AddItem(charm);
        GameDictionary.Instance.UpdateEntry("Shinzo Fragment", false);
        GameDictionary.Instance.UpdateEntry("Acala's Charm", true);
        completeShrine.SetActive(true);
        this.gameObject.SetActive(false);

    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
    }


}
