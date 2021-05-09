using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaimyoDialogueTrigger : DialogueTrigger
{
    public GameObject sasaki;
    public bool offeredSakeChoice;
    public bool accusedShigieChoice;
    public bool choicesShown;
    public bool talkedSake;
    public bool accusedShigie;
    public bool shigieDialogue;
    public bool exchangeOver;

    
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
            if (GameDictionary.choiceDictionary["Duel Entry"] && dialogueEnd && !choicesShown)
            {
                dialogueEnd = false;

                if(GameDictionary.choiceDictionary["Sake Cup"] && GameDictionary.choiceDictionary["Sake Bottle"])
                {
                    DecisionDisplay("Offer a Drink of Sake", "Plead with the Daimyo");
                    offeredSakeChoice = true;
                }

                else if(GameDictionary.choiceDictionary["Blooded Tanto"])
                {
                    DecisionDisplay("Accuse Ishida Shigie of Murder", "Plead with the Daimyo");
                    accusedShigieChoice = true;
                }

                else
                {
                    DecisionDisplay("???", "Plead with the Daimyo");
                }

            }

            if (GameDictionary.choiceDictionary["Beggar Entry"] && dialogueEnd && !choicesShown)
            {
                dialogueEnd = false;
                

                if(GameDictionary.choiceDictionary["Sake Cup"] && GameDictionary.choiceDictionary["Sake Bottle"])
                {
                    DecisionDisplay("Offer a Drink of Sake", "Plead with the Daimyo");
                    offeredSakeChoice = true;
                }

                else if(GameDictionary.choiceDictionary["Blooded Tanto"])
                {
                    DecisionDisplay("Accuse Ishida Shigie of Murder", "Plead with the Daimyo");
                    accusedShigieChoice = true;
                }
                
                else
                {
                    DecisionDisplay("???", "Plead with the Daimyo");
                }
            }

            if (GameDictionary.choiceDictionary["Monk Entry"] && dialogueEnd && !choicesShown)
            {
                dialogueEnd = false;

                if(GameDictionary.choiceDictionary["Sake Cup"] && GameDictionary.choiceDictionary["Sake Bottle"])
                {
                    DecisionDisplay("Offer a Drink of Sake", "Plead with the Daimyo");
                    offeredSakeChoice = true;
                }

                else if(GameDictionary.choiceDictionary["Blooded Tanto"])
                {
                    DecisionDisplay("Accuse Ishida Shigie of Murder", "Plead with the Daimyo");
                    accusedShigieChoice = true;
                }
                
                else
                {
                    DecisionDisplay("???", "Plead with the Daimyo");
                }
            }

            if (GameDictionary.choiceDictionary["Left Stronghold"] && dialogueEnd && !choicesShown)
            {
                dialogueEnd = false;

                if(GameDictionary.choiceDictionary["Sake Cup"] && GameDictionary.choiceDictionary["Sake Bottle"])
                {
                    DecisionDisplay("Offer a Drink of Sake", "Plead with the Daimyo");
                    offeredSakeChoice = true;
                }

                if(GameDictionary.choiceDictionary["Blooded Tanto"])
                {
                    DecisionDisplay("Accuse Ishida Shigie of Murder", "Plead with the Daimyo");
                    accusedShigieChoice = true;
                }
                
                else
                {
                    DecisionDisplay("Counsel Peace and Diplomacy", "Counsel Use of Force");
                }
            }

            if(dialogueEnd && choicesShown && !shigieDialogue)
            {
                dialogueEnd = false;

                if(talkedSake)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[8];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }

                else if(accusedShigie)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[9];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }

                else
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[10];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }
                shigieDialogue = true;
            }

            if(dialogueEnd && choicesShown && shigieDialogue)
            {
                dialogueEnd = false;

                if(talkedSake)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[11];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }

                else if(accusedShigie)
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[12];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }

                else
                {
                    button.GetComponent<DialogueRun>().dialogue = Dialogues[13];
                    button.GetComponent<DialogueRun>().trigger = this;
                    button.GetComponent<DialogueRun>().TriggerDialogue();
                }
                exchangeOver = true;
            }

            if(exchangeOver)
            {
                //load the next scene
                //SceneLoader.Load()
            }
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);

            if (GameDictionary.choiceDictionary["Samurai Path"] && !GameDictionary.choiceDictionary["Left Stronghold"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
                sasaki.GetComponent<NPCFollowPlayerScript>().onSwitch = false;
                sasaki.GetComponent<NPCFollowPlayerScript>().StopMotion();
                GameDictionary.Instance.UpdateEntry("Daimyo Service", true);
            }

            if (GameDictionary.choiceDictionary["Duel Entry"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if (GameDictionary.choiceDictionary["Beggar Entry"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if (GameDictionary.choiceDictionary["Monk Entry"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[3];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if (GameDictionary.choiceDictionary["Trained as Samurai"] && !GameDictionary.choiceDictionary["Left Stronghold"])
            {
                //pick which Dialogue to run
                button.GetComponent<DialogueRun>().dialogue = Dialogues[4];
                button.GetComponent<DialogueRun>().trigger = this;
            }

            if(GameDictionary.choiceDictionary["Left Stronghold"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[14];
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
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);

        if(offeredSakeChoice)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[5];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
        }

        else if(accusedShigieChoice)
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[6];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
        }

        else
        {
            button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
            button.GetComponent<DialogueRun>().trigger = this;
            button.GetComponent<DialogueRun>().TriggerDialogue();
        }
    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        button.GetComponent<DialogueRun>().dialogue = Dialogues[7];
        button.GetComponent<DialogueRun>().trigger = this;
        button.GetComponent<DialogueRun>().TriggerDialogue();
    }


}
