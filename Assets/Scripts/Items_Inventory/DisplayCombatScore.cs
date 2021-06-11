using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCombatScore : MonoBehaviour
{
    public Text text;
    public GameObject player;
    void Update()
    {
        text.text = player.GetComponent<StateTracker>().combatScore.ToString();
    }
}
