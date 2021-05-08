using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GinkgoTreeTrigger : DialogueTrigger
{
    public GameObject player;
    public GameObject ginkgoSapling;


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
        if(collider.CompareTag("Player"))
        {
        inRange = true;
        player = collider.gameObject;

            if(GameDictionary.choiceDictionary["Ginkgo Seed"])
            {
                button.GetComponent<DialogueRun>().trigger = this;
                DecisionDisplay("Plant the Ginkgo Seed", "Do Nothing");
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
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
        player.GetComponent<PlayerController>().smallItem = null;
        GameDictionary.Instance.UpdateEntry("Planted Ginkgo", true);
        GameDictionary.Instance.UpdateEntry("Ginkgo Seed", false);
        ginkgoSapling.SetActive(true);
    }

    public override void ButtonB()
    {
        this.buttonA.SetActive(false);
        this.buttonB.SetActive(false);
    }


}
