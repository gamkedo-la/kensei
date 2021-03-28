using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillerVillagerDialogue : DialogueTrigger
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
                int randomInt = Random.Range(1, 15);
                Dialogue dialogue = GetComponent<Dialogue>();

                switch (randomInt)
                {

                    case 1:
                        dialogue.sentences[0] = "It is lovely weather this time of year.";
                        break;

                    case 2:
                        dialogue.sentences[0] = "How do they explain these taxes? We cannot afford them.";
                        break;

                    case 3:
                        dialogue.sentences[0] = "I always wanted to be a samurai, but I have a bad knee.";
                        break;

                    case 4:
                        dialogue.sentences[0] = "It would be nice to travel outside of the village. Life is too stale here.";
                        break;

                    case 5:
                        dialogue.sentences[0] = "I like the peace and quiet of the village. Every place has problems, but I can live with these.";
                        break;

                    case 6:
                        dialogue.sentences[0] = "I challenge you to a duel!! Haha just kidding, you'd totally beat me!";
                        break;

                    case 7:
                        dialogue.sentences[0] = "Travelling is overrated. Let the world come to you.";
                        break;
                    case 8:
                        dialogue.sentences[0] = "I should try a new recipe tonight. I got one from my neighbor.";
                        break;
                    case 9:
                        dialogue.sentences[0] = "I'm afraid for my life after what happened to Mitsu... to think someone was murdered in the night!";
                        break;
                    case 10:
                        dialogue.sentences[0] = "The town feels on edge recently. I could really use some sake.";
                        break;
                    case 11:
                        dialogue.sentences[0] = "Between you and me, I don’t care for the village elder. Just because you live a long time does not mean you are a suitable leader.";
                        break;
                    case 12:
                        dialogue.sentences[0] = "Another round of taxes? Who can afford that!";
                        break;
                    case 13:
                        dialogue.sentences[0] = "My vegetables aren’t coming in as well as I would like this season.";
                        break;
                    case 14:
                        dialogue.sentences[0] = "Poor Mitsu. She didn't deserve to go before her time. They never caught who did it";
                        break;
                    case 15:
                        dialogue.sentences[0] = "It's so sad and scary how Mitsu was murdered! To think it could have been any of us...";
                        break;
                }

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

    }

    public override void ButtonB()
    {

    }


}
