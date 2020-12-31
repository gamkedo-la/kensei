using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
   
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;
    
    public CircleCollider2D collider;
    public GameObject button;
    public GameObject panel;
    private bool inRange;
    public bool dialogueEnd;
    public GameObject combatScore;

    public GameObject buttonA;
    public GameObject buttonB;
    bool isNude;
    bool oneArm;

    public void Start()
    {
       button.SetActive(false); 
       buttonA.SetActive(false);
       buttonB.SetActive(false);
       combatScore.SetActive(false);
    }

    void Update()
    {
        if(dialogueEnd && !isNude && !oneArm)
        {
            DecisionDisplay();
            dialogueEnd = false;
        }
        else dialogueEnd = false;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        inRange = true;
        Debug.Log("inrange");
        button.SetActive(true);

        if(!collider.GetComponent<StateTracker>().currentClothing)
        {
            if(!collider.GetComponent<StateTracker>().oneArm)   
            { 
            button.GetComponent<DialogueRun>().dialogue = dialogue2;
            button.GetComponent<DialogueRun>().trigger = this;
            isNude = true;
            oneArm = false;
            }
            else
            {
            button.GetComponent<DialogueRun>().dialogue = dialogue3;
            button.GetComponent<DialogueRun>().trigger = this;
            isNude = true;
            oneArm = true;
            }
        }
        else
        {
            if(!collider.GetComponent<StateTracker>().oneArm)
            {
            button.GetComponent<DialogueRun>().dialogue = dialogue1;
            button.GetComponent<DialogueRun>().trigger = this;
            isNude = false;
            oneArm = false;
            }
            else
            {
            button.GetComponent<DialogueRun>().dialogue = dialogue4;
            button.GetComponent<DialogueRun>().trigger = this;
            isNude = false;  
            oneArm = true;
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D collider)
    {
        inRange = false;
        Debug.Log("ootrange");
        button.SetActive(false);
        button.GetComponent<DialogueRun>().dialogue = null;
        button.GetComponent<DialogueRun>().trigger = null;
        dialogueEnd = false;
        panel.SetActive(false);
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        combatScore.SetActive(false);
    }

    public void DecisionDisplay()
    {
        buttonA.SetActive(true);
        buttonB.SetActive(true);
        combatScore.SetActive(true);
    }


}
