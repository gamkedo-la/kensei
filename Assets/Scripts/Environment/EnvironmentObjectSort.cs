using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjectSort : MonoBehaviour
{
    public BoxCollider2D collider;
    // Update is called once per frame
    public void Start()
    {
       GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
       GetComponent<SpriteRenderer>().sortingOrder = 10; 
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
       GetComponent<SpriteRenderer>().sortingOrder = 1; 
    }
}
