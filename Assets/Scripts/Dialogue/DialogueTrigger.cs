using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    
    public List<Dialogue> Dialogues;
    public CircleCollider2D collider;
    public GameObject button;
    public GameObject panel;
    public bool inRange;
    public bool dialogueEnd;
    public GameObject combatScore;
    public GameObject buttonA;
    public string buttonAText;
    public GameObject buttonB;
    public string buttonBText;

    public virtual void Start()
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

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") == true)
        {
        inRange = true;
        button.SetActive(true);

            if(/*some condition*/true)
            {
            //pick which Dialogue to run
            button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
            button.GetComponent<DialogueRun>().trigger = this;
            } 
        }  
    }
    
    public virtual void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") == true)
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

    public virtual void DecisionDisplay(string buttonAText, string buttonBText)
    {
        this.buttonA.GetComponentInChildren<Text>().text = buttonAText;
        this.buttonB.GetComponentInChildren<Text>().text = buttonBText;
        this.buttonA.SetActive(true);
        this.buttonB.SetActive(true);
    }

    public virtual void ButtonA()
    {

    }

    public virtual void ButtonB()
    {

    }
   
}
