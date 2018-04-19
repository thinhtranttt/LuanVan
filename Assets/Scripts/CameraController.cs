using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform player;
    private bool isFirst = true;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (isFirst)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                isFirst = false;
                Debug.Log("smdhfsdmhfb");
            }
            Vector3 tmp = player.position;
            tmp.y = transform.position.y;
            transform.position = tmp;
        }
       
	}
}
