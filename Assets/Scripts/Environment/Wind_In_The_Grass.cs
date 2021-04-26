using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_In_The_Grass : MonoBehaviour
{
    public float windSpeedMin=3.5f;
    public float windSpeedMax=4f;
	public float windStrengthMin=0.75f;
    public float windStrengthMax=1.25f;
	
    private float windSpeed=4f;
	private float windStrength=0.75f;
    private float rotstarting=0f;
    private float timeoffset=0f; 
    
    void Start()
    {
		rotstarting = transform.localRotation.z;
		timeoffset = Random.Range(-5f, 5f);
        windSpeed = Random.Range(windSpeedMin, windSpeedMax);
        windStrength = Random.Range(windStrengthMin, windStrengthMax);
    }

    void Update()
    {
   		float r=rotstarting+windStrength*Mathf.Sin(windSpeed*Time.time+timeoffset);
		transform.localRotation = Quaternion.Euler(transform.localRotation.x,transform.localRotation.y,r);
    }
}
