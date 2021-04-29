using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForTitle());
    }

    // Update is called once per frame


    IEnumerator WaitForTitle()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
