using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("WaitForAnim");
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
