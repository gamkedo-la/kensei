using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameSimpleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public GameObject targetPosition;
    public float speed;
    public bool onSwitch;
    private Vector2 targetVector;
    public Animator animator;
    public GameObject daimyo;
    public GameObject button;

    void Update()
    {
        Vector2 Delta = new Vector2(targetPosition.transform.position.x - transform.position.x, targetPosition.transform.position.y - transform.position.y);
        targetVector = Delta.normalized;

        if (onSwitch)
        {
            if (targetVector.y > 0.1f)
            {
                if (animator) animator.SetInteger("Direction", 3);
            }
            else if (targetVector.y < -0.1f)
            {
                if (animator) animator.SetInteger("Direction", 0);
            }

            if(Delta.magnitude > 1)
            {
                rb.MovePosition(rb.position+targetVector*speed*Time.fixedDeltaTime);
                if(animator) animator.SetFloat("Speed", targetVector.magnitude * speed * Time.fixedDeltaTime);
            }

            else 
            {
                animator.SetFloat("Speed", 0f);
                if (animator) animator.SetInteger("Direction", 0);
                onSwitch = false;
                if (GameDictionary.choiceDictionary["Ronin Path"] || GameDictionary.choiceDictionary["Samurai Path"])
                {
                button.GetComponent<DialogueRun>().trigger = daimyo.GetComponent<DaimyoDestroyedVillageDialogueTrigger>();
                daimyo.GetComponent<DaimyoDestroyedVillageDialogueTrigger>().DecisionDisplay("Save the Daimyo", "Do Nothing");
                }
                else
                {
                    button.GetComponent<DialogueRun>().trigger = daimyo.GetComponent<DaimyoDestroyedVillageDialogueTrigger>();
                    daimyo.GetComponent<DaimyoDestroyedVillageDialogueTrigger>().dialogueEnd = true;
                }
            }
        }
    }

    void StopMotion()
    {
        animator.SetFloat("Speed", 0f);
        if (animator) animator.SetInteger("Direction", 0);
    }
}
