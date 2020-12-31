using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonB : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject combatScore;
    public void NotAmputation()
    {
        Debug.Log("notamputated");
        button1.SetActive(false);
        button2.SetActive(false);
        combatScore.SetActive(false);
    }
}
