﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    public GameObject panel;
    public GameObject button;
    public GameObject choiceA;
    public GameObject choiceB;
    public GameObject combatScore;
    public DialogueTrigger trigger;
    public Text nameText;
    public Text dialogueText;

    public bool forceLock;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        panel.SetActive(false);
        choiceA.SetActive(false);
        choiceB.SetActive(false);
        combatScore.SetActive(false);
        button.SetActive(false);
    }

    void Update()
    {
        if(panel.activeSelf == false && choiceA.activeSelf == false && FindObjectsOfType<PlayerController>()[0].movementLocked && !forceLock)
        {
            FindObjectsOfType<PlayerController>()[0].movementLocked = false;
        } 
        else if(panel.activeSelf == false && choiceA.activeSelf == true && !FindObjectsOfType<PlayerController>()[0].movementLocked && !forceLock)
        {
            FindObjectsOfType<PlayerController>()[0].movementLocked = true;
        } 
    }

    public void StartDialogue(Dialogue dialogue)
    {
        FindObjectsOfType<PlayerController>()[0].movementLocked = true;

        sentences.Clear();
        panel.SetActive(true);
        button.SetActive(false);
        if (nameText!=null) nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("Button Push");
        if (sentences.Count == 0)
        {
            Debug.Log("End D");
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        panel.SetActive(false);
        if (trigger!=null) trigger.dialogueEnd = true;
        FindObjectsOfType<PlayerController>()[0].movementLocked = false;

    }

}
