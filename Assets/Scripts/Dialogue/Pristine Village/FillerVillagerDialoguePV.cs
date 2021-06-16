using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillerVillagerDialoguePV : DialogueTrigger
{

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
            button.SetActive(true);

            if (!GameDictionary.choiceDictionary["Nude"])
            {
                int randomInt = Random.Range(1, 5);
                Dialogue dialogue = GetComponent<Dialogue>();

                switch (randomInt)
                {

                    case 1:
                        dialogue.sentences[0] = "There was a tension in the air, but it seems to have subsided.";
                        break;

                    case 2:
                        dialogue.sentences[0] = "The Daimyo’s blossom on Mitsu's grave really shows us he cares about us.";
                        break;

                    case 3:
                        dialogue.sentences[0] = "I want to be a samurai someday. I want to serve and protect our village.";
                        break;

                    case 4:
                        dialogue.sentences[0] = "Seems like they will let anyone become a samurai these days.";
                        break;

                    case 5:
                        dialogue.sentences[0] = "It is good the Daimyo and Musashi are respecting one another for now. I was afraid this would not be a peaceful interaction.";
                        break;
                }

                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
            }
            else
            {
                Dialogue dialogue = GetComponent<Dialogue>();
                dialogue.sentences[0] = "Maybe you should get dressed...";
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
