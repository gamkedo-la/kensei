using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    public float eyesOpenMinTime = 2f;
    public float eyesOpenMaxTime = 5f;
    public float eyesClosedMinTime = 0.1f;
    public float eyesClosedMaxTime = 0.25f;

    SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("Blinking");
    }

    IEnumerator Blinking() {
        for(;;) {
            float delay = 1f;
            if (m_SpriteRenderer) {
                // if eyes are open
                if (!m_SpriteRenderer.enabled) {
                    // fast blink
                    m_SpriteRenderer.enabled = true;
                    delay = Random.Range(eyesClosedMinTime,eyesClosedMaxTime);
                } else { 
                    // open and for longer
                    m_SpriteRenderer.enabled = false;
                    delay = Random.Range(eyesOpenMinTime,eyesOpenMaxTime);
                }
            }
             
            yield return new WaitForSeconds(delay);
        }
    }
}
