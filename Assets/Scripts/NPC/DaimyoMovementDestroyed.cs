using System.Collections;
using UnityEngine;

public class DaimyoMovementDestroyed : NPCFollowPath
{
    private bool continueMoving = false;


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
