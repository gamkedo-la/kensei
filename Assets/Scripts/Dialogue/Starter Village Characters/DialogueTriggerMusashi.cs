using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerMusashi : DialogueTrigger
{
    public GameObject arm;
    public GameObject screenEffect;
    public GameObject particles;
    public bool challengingDialogue = false;
    public override void Start()
    {
        button.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    void Update()
    {
        if (dialogueEnd && challengingDialogue)
        {
            DecisionDisplay("Challenge", "Walk Away");
            dialogueEnd = false;
            challengingDialogue = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
            //check for player
            if (collider.CompareTag("Player"))
            {
                inRange = true;

                button.SetActive(true);

                if (!GameDictionary.choiceDictionary["Nude"])
                {
                    if (!GameDictionary.choiceDictionary["One Arm"])
                    {
                        button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                        button.GetComponent<DialogueRun>().trigger = this;
                        challengingDialogue = true;

                    }
                    else
                    {
                        button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                        button.GetComponent<DialogueRun>().trigger = this;
                    }
                }
                else
                {
                    if (!GameDictionary.choiceDictionary["One Arm"])
                    {
                        button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                        button.GetComponent<DialogueRun>().trigger = this;

                    }
                    else
                    {
                        button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                        button.GetComponent<DialogueRun>().trigger = this;

                    }
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
            challengingDialogue = false;
            panel.SetActive(false);
            buttonA.SetActive(false);
            buttonB.SetActive(false);

        }
    }

    public override void DecisionDisplay(string buttonAText, string buttonBText)
    {
        buttonA.GetComponentInChildren<Text>().text = buttonAText;
        buttonB.GetComponentInChildren<Text>().text = buttonBText;
        buttonA.SetActive(true);
        buttonB.SetActive(true);


    }
    public override void ButtonA()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);

        GameDictionary.choiceDictionary["One Arm"] = true;
        arm.SetActive(true);
        screenEffect.SetActive(true);
        FindObjectOfType<PlayerController>().ChooseAnimator();
    }
    public override void ButtonB()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);

        //particles.SetActive(true);
        button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
    }

}
