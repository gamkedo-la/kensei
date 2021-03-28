using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBasedLoad : MonoBehaviour
{
    public Collider2D collider2D;
    public SceneLoader.Scene scene;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player")) SceneLoader.Load(scene);
    }
}
