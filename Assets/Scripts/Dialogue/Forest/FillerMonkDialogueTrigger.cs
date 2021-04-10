using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillerMonkDialogueTrigger : DialogueTrigger
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
                int randomInt = Random.Range(1, 7);
                Dialogue dialogue = GetComponent<Dialogue>();

                switch (randomInt)
                {

                    case 1:
                        dialogue.sentences[0] = "Have you ever just listened to nature? It can tell us so much with no words.";
                        break;

                    case 2:
                        dialogue.sentences[0] = "My morning prayers to the temple statues felt particularly uplifting this morning.";
                        break;

                    case 3:
                        dialogue.sentences[0] = "I took some vegetables to a family in need. They seemed very appreciative.";
                        break;

                    case 4:
                        dialogue.sentences[0] = "I expected this lifestyle to be boring, but I have never felt this calm.";
                        break;

                    case 5:
                        dialogue.sentences[0] = "I had a nice tsukimairi this morning.";
                        break;

                    case 6:
                        dialogue.sentences[0] = "Given their loss, the family I met with during their memorial seemed at peace.";
                        break;

                    case 7:
                        dialogue.sentences[0] = "Have you sat outside, closed your eyes and visualized your breath like the wind around you?";
                        break;
                    
                }
    

                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
            }
            else
            {
                Dialogue dialogue = GetComponent<Dialogue>();
                dialogue.sentences[0] = "By Buddha's belly! Are you possesed by a Yokai?";
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

    }

    public override void ButtonB()
    {

    }


}
