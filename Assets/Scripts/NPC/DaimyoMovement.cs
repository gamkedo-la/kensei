using System.Collections;
using UnityEngine;

public class DaimyoMovement : NPCFollowPath
{
    public GameObject sakuraBlossom;
    public bool dropSakuras;
    private bool droppedSakuras = false;
    private bool continueMoving = false;
    public DialogueTrigger graveTrigger;

    protected override void Start()
    {
        base.Start();
        if(gameObject.CompareTag("Player")){
            GetComponent<PlayerController>().movementLocked = true;
        }
    }

    protected override void Update() {
        base.Update(); // run the original update first
        if(targetIndex == 3 && gameObject.CompareTag("Player")){ // Enable collider for player before arriving to the grave
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
        if(targetIndex == 4 && !droppedSakuras) // Reached the grave, movement will be stopped for 2 secs
        {
            stopped = true;
            if(graveTrigger.dialogueEnd)
            {
            DropSakuras();
            }
        }
    }

    void DropSakuras() {
        droppedSakuras = true;
        animator.SetFloat("Speed", 0f);
        if(dropSakuras) Instantiate(sakuraBlossom, transform.position, Quaternion.identity); // Only drop sakuras if you are the daimyo
        stopped = false;
        if(gameObject.GetComponent<PlayerController>())
        {
        GetComponent<PlayerController>().movementLocked = true;
        }
    }

    public void ContinueMoving() {
        stopped = false;
    }

    protected override void FinishMoving()
    {
        base.FinishMoving();
        if(gameObject.CompareTag("Player")){
            GetComponent<PlayerController>().movementLocked = false;
        } else {
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
