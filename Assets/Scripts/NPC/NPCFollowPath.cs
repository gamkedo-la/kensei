using UnityEngine;

public class NPCFollowPath : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected bool finishedMovement;
    protected bool stopped;
    protected Vector2 nextTarget;
    protected int finalPoint;
    protected int targetIndex;
    public float speed;
    public Transform paths;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        finishedMovement = false;
        nextTarget = paths.GetChild(0).transform.position;
        targetIndex = 0;
        finalPoint = paths.childCount;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(stopped)
        {
            if (animator) animator.SetFloat("Speed", 0f);
        }
        if (!finishedMovement && !stopped)
        {
            Vector2 delta = new Vector2(nextTarget.x - transform.position.x, nextTarget.y - transform.position.y);
            if (delta.y > 0.1f)
            {
                if (animator) animator.SetInteger("Direction", 3);
                if (gameObject.CompareTag("Player"))
                {
                    GetComponent<PlayerController>().facingUp = true;
                }
            }
            else if (delta.y < -0.1f)
            {
                if (animator) animator.SetInteger("Direction", 0);
                if (gameObject.CompareTag("Player"))
                {
                    GetComponent<PlayerController>().facingUp = false;
                }
            }

            if (delta.magnitude > 1)
            {
                rb.MovePosition(rb.position + delta.normalized * speed * Time.fixedDeltaTime);
                if (animator) animator.SetFloat("Speed", nextTarget.magnitude * speed * Time.fixedDeltaTime);
            }
            else
            {
                targetIndex++;
                if (targetIndex > finalPoint - 1) // Finished moving
                {
                    FinishMoving();
                    return;
                }
                nextTarget = paths.GetChild(targetIndex).transform.position;
            }
        }
    }

    protected virtual void FinishMoving()
    {
        finishedMovement = true;
        if (animator)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetInteger("Direction", 0); // face downwards after moving
        }
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
