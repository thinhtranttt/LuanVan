using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesy : MonoBehaviour {

    private GameObject grvCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectWithTag("HeadCam") == null)
        {
        }
        else
        {
            grvCam = GameObject.FindGameObjectWithTag("HeadCam");
            transform.rotation = grvCam.transform.rotation;
        }
	}
}
