using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTestScript : MonoBehaviour
{  
    public Animator animator;
    public Rigidbody2D rb;
    private int direction = 1;
    public float speed = 5f;
    private int inc = 0;

    void FixedUpdate()
    {
        Vector2 targetVector = new Vector2(1,1);
        rb.velocity = targetVector*speed;

        if(rb.velocity.y>0.1)
        {
            animator.SetInteger("Direction", 3);
        }
        else{animator.SetInteger("Direction", 0);}

        animator.SetFloat("Speed", Mathf.Sqrt(speed*speed));
        inc++;

        if(inc > 200)
        {
        inc = 0;
        speed = speed*-1;
        }
    }   
}
