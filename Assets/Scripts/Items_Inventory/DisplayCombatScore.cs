using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCombatScore : MonoBehaviour
{
    public Text text;
    public int combatScore;
    public GameObject player;
    void Update()
    {
        combatScore = player.GetComponent<StateTracker>().combatScore;
        text.text = combatScore.ToString();
    }
}
