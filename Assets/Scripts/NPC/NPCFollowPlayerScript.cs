using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCFollowPlayerScript : MonoBehaviour
{
    public Text text;
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
    private float minDist = 7;
    private float maxDist = 12; 
    private float targetDist = 10;


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
            //text.text = "Stopped";
            animator.SetFloat("Speed", 0f);
            //animator.SetInteger("Direction", 0);
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
                //text.text = "Moving";
                // Update our progress to our destination
                Vector2 Delta = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
                targetVector = Delta.normalized;

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

                if(reached == false)
                {

                    if(targetVector.y > 0.1f)
                    {
                        animator.SetInteger("Direction", 3);
                    }
                    else if(targetVector.y < -0.1f)
                    {
                        animator.SetInteger("Direction", 0);
                    }

                }
            }
        }
    }

    void FollowPlayer()
    {
        //text.text = "Followed";
        //figure out where the player is 
        float dist = Vector2.Distance(player.transform.position, transform.position);
        float goalDist = Vector2.Distance(destination, transform.position);
        
        //for testing
        if(text != null) text.text = ""+dist;

        if(dist < minDist)
        {
            reached = false;
            Vector2 point = player.transform.InverseTransformPoint(transform.position);
            destination = player.transform.TransformPoint(point.normalized * targetDist);
        }

        else if(dist > maxDist)
        {
            reached = false;
            Vector2 point = player.transform.InverseTransformPoint(transform.position);
            destination = player.transform.TransformPoint(point.normalized * targetDist);
        }

        else if( goalDist < 0.5f)
        {
            destination = transform.position;

            if(reached == false) 
            {
                animator.SetInteger("Direction", 0);
            }
            reached = true;
        }
    }

   
  
}
