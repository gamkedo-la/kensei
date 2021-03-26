using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBasedLoad : MonoBehaviour
{
    public CircleCollider2D circle;
    public SceneLoader.Scene scene;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player")) SceneLoader.Load(scene);
    }
}
