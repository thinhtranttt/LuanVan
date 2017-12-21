using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour {

    public float time = 0.05f;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right, time * Time.deltaTime);
        
	}
}
