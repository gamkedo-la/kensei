using UnityEngine;

public class PristineVEGrandDaughterDialogueScript : DialogueTrigger
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
            if (GameDictionary.choiceDictionary["Ronin Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[0];
                button.GetComponent<DialogueRun>().trigger = this;
            }
            else if (GameDictionary.choiceDictionary["Monk Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[1];
                button.GetComponent<DialogueRun>().trigger = this;
            }
            else if (GameDictionary.choiceDictionary["Samurai Path"])
            {
                button.GetComponent<DialogueRun>().dialogue = Dialogues[2];
                button.GetComponent<DialogueRun>().trigger = this;
            }
        }
    }
}
