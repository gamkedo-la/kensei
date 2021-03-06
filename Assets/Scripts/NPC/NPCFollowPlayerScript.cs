using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowPlayerScript : MonoBehaviour
{
    public bool onSwitch;
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;

    // Destination of our current move
    private Vector2 destination;
    private Vector2 targetVector;


    // Start of our current move
    bool reached = false;
    bool stopped = false;
    bool invoked = false;

    private GameObject player;

    private float lastCheckTime;

    private float xSeconds = .08f;
    private Vector3 lastCheckPos;


    // Use this for initialization
    void Start () {
        onSwitch = true;
        destination = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            lastCheckPos = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update() 
    {
        if(onSwitch)
        {
            if(stopped)
            {

            reached = false;
            animator.SetFloat("Speed", 0f);
            animator.SetInteger("Direction", 0);
            rb.velocity = new Vector3(0f,0f,0f);

                if ((Time.time - lastCheckTime) > xSeconds)
                {
                    if(player.transform.position == lastCheckPos)
                    {
                        stopped = true;
                    }
                    else{stopped = false;}

                    lastCheckPos = player.transform.position;
                    lastCheckTime = Time.time;
                        
                }
            }
            else
            {
                // Update our progress to our destination
                Vector2 Delta = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
                targetVector = Delta.normalized;
                
                // Check for the case when we overshoot or reach our destination
                if (Delta.magnitude < 0.5f)
                {
                    reached = true;
                }

                if ((Time.time - lastCheckTime) > xSeconds)
                {
                    if(player.transform.position == lastCheckPos && reached)
                    {
                        stopped = true;
                    }
                    else{stopped = false;}

                    lastCheckPos = player.transform.position;
                    lastCheckTime = Time.time;
                        
                }
    
                // Update our position based on our start postion, destination and progress.
                FollowPlayer();
                rb.MovePosition(rb.position+targetVector*speed*Time.fixedDeltaTime);
                animator.SetFloat("Speed", targetVector.magnitude * speed * Time.fixedDeltaTime);

                if(targetVector.y > 0f)
                {
                    animator.SetInteger("Direction", 3);
                }
                else{animator.SetInteger("Direction", 0);}
            }
        }
    }

    void FollowPlayer()
    {
        //figure out where the player is 
        Vector2 point = player.transform.InverseTransformPoint(transform.position);
        destination = player.transform.TransformPoint(point.normalized * 7f);
    }

    IEnumerator Chill()
    {
        invoked = true;
        animator.SetFloat("Speed", 0f);
        animator.SetInteger("Direction", 0);
        rb.velocity = new Vector3(0f,0f,0f);
        yield return new WaitForSeconds(0.5f);
        stopped = false;
        invoked = false;
        FollowPlayer();
    }
  
}
