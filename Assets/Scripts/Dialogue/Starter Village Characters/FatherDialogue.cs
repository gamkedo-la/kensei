using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatherDialogue : DialogueTrigger
{

    public GameObject player;
    public GameObject sakeBottle;
    private bool giveSakeBottle = false;

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //check for conditions for different dialogue options
        if (dialogueEnd && !GameDictionary.choiceDictionary["Given Sake Bottle"] && !GameDictionary.choiceDictionary["Nude"])
        {
            DecisionDisplay("Take Sake Bottle", "No Thanks");
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            player = collider.gameObject;
            inRange = true;
            button.SetActive(true);

            if (!GameDictionary.choiceDictionary["Nude"])
            {
                if (!GameDictionary.choiceDictionary["Given Sake Bottle"] && 
                    !GameDictionary.choiceDictionary["One Arm"])
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                    button.GetComponent<DialogueRun>().trigger = this;
                    giveSakeBottle = true;
                }
                else
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                    button.GetComponent<DialogueRun>().trigger = this;
                }
            }
            else
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") == true)
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
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
        GameDictionary.choiceDictionary["Given Sake Bottle"] = true;
        player.GetComponent<PlayerController>().AddItem(sakeBottle);
    }

    public override void ButtonB()
    {
        dialogueEnd = false;
        panel.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }
}
