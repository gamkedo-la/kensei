using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonA : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject combatScore;
    public GameObject player;
    public GameObject arm;
    public void Amputation()
    {
        Debug.Log("amputated");
        button1.SetActive(false);
        button2.SetActive(false);
        combatScore.SetActive(false);
        player.GetComponent<StateTracker>().oneArm = true;
        arm.SetActive(true);
    }
}
