using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatherDialogue : DialogueTrigger
{

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
        if (dialogueEnd)
        {
            dialogueEnd = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            inRange = true;
            button.SetActive(true);

            if (!GameDictionary.choiceDictionary["Nude"])
            {
                if (!GameDictionary.choiceDictionary["One Arm"])
                {
                    Debug.Log("Arm FALSE");
                    Debug.Log(GameDictionary.choiceDictionary["One Arm"]);
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                    button.GetComponent<DialogueRun>().trigger = this;
                }
                else
                {
                    Debug.Log("One Arm TRUE");
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
}
