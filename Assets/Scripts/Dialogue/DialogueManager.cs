using System.Collections;
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

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        panel.SetActive(true);
        button.SetActive(false);
        nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        panel.SetActive(false);
        trigger.dialogueEnd = true;
    }

}
