using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWander : MonoBehaviour
{
    public bool onSwitch;
    public float radius;
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;
    // The point we are going around in circles
    private Vector2 basestartpoint;

    // Destination of our current move
    private Vector2 destination;

    // Start of our current move
    private Vector2 start;
    bool reached = false;
    bool stopped = false;
    bool invoked = false;
    private float lastCheckTime;

    private float xSeconds = 1;
    private Vector3 lastCheckPos;
    private System.Random random;


    // Use this for initialization
    void Start () {
        onSwitch = true;
        start = transform.localPosition;
        basestartpoint = transform.localPosition;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PickNewRandomDestination();
        random = new System.Random();
    }

    // Update is called once per frame
    void Update () {
        if(onSwitch)
        {
            if(stopped)
            {
                if(!invoked)
                {
                StartCoroutine(StopAndStare());
                }
            }

            else
            {
                // Update our progress to our destination
                Vector2 Delta = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
                // Check for the case when we overshoot or reach our destination

                if (Delta.magnitude < 0.1)
                {
                    reached = true;
                }

                Vector2 targetVector = Delta.normalized;
                // Update our position based on our start postion, destination and progress.
                rb.MovePosition(rb.position+targetVector*speed*Time.fixedDeltaTime);
                animator.SetFloat("Speed", targetVector.magnitude * speed * Time.fixedDeltaTime);

                if(targetVector.y > 0)
                {
                    animator.SetInteger("Direction", 3);
                }
                else{animator.SetInteger("Direction", 0);}
        
                // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
                if (reached)
                {
                    start = destination;
                    reached = false;
                    stopped = true;
                }

                if (!stopped && (Time.time - lastCheckTime) > xSeconds)
                {
                    Vector3 stuckVector = new Vector3(rb.transform.position.x - lastCheckPos.x, rb.transform.position.y - lastCheckPos.y, rb.transform.position.z - lastCheckPos.z);
                    if (stuckVector.magnitude < 1) PickNewRandomDestination();
                        
                    lastCheckPos = rb.transform.position;
                    lastCheckTime = Time.time;
                        
                }
            }
        }
    }

    void PickNewRandomDestination()
    {
        // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
        destination = Random.insideUnitCircle * radius + basestartpoint;
    }

    IEnumerator StopAndStare()
    {
        int seconds = random.Next(2,5);
        invoked = true;
        animator.SetFloat("Speed", 0f);
        animator.SetInteger("Direction", 0);
        rb.velocity = new Vector3(0f,0f,0f);
        yield return new WaitForSeconds(seconds);
        stopped = false;
        invoked = false;
        PickNewRandomDestination();
    }
  
}
