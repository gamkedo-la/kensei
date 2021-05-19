using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaimyoMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool finishedMovement;
    private bool stopped;
    private Vector2 nextTarget;
    private int targetIndex;
    public float speed;
    public Transform paths;
    public GameObject sakuraBlossom;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        finishedMovement = false;
        nextTarget = paths.GetChild(0).transform.position;
        targetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!finishedMovement && !stopped) {

            Vector2 delta = new Vector2(nextTarget.x - transform.position.x, nextTarget.y - transform.position.y);

            if(delta.magnitude > 1)
            {
                rb.MovePosition(rb.position+ delta.normalized * speed * Time.fixedDeltaTime);
                if(animator) animator.SetFloat("Speed", nextTarget.magnitude * speed * Time.fixedDeltaTime);
            }
            else
            {
                targetIndex++;
                if(targetIndex == 4) // Reached the grave, movement will be stopped for 2 secs
                {
                    StartCoroutine("DropSakuras");
                }
                if(targetIndex == 7) // Finished moving
                {
                    finishedMovement = true;
                    animator.SetFloat("Speed", 0f);
                    return;
                }

                nextTarget = paths.GetChild(targetIndex).transform.position;
                if (animator) animator.SetInteger("Direction", 0);
            }
        }
    }

    IEnumerator DropSakuras() {
        stopped = true;
        yield return new WaitForSeconds(2);
        Instantiate(sakuraBlossom, transform.position, Quaternion.identity);
        stopped = false;
    }
}
