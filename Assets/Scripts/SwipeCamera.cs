using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SwipeCamera : NetworkBehaviour {

	private Touch initTouch = new Touch ();
	public Camera cam;
	public Transform player;
    public Transform joystick;

	private float rotX = 0f;
	private float rotY = 0f;
	public Vector3 oriRot = Vector3.zero;

	public float rotSpeed = 0.1f;
	public float dir = -1;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            Destroy(this);
            return;
        }
        cam = Camera.main;
		oriRot = cam.transform.eulerAngles;
		player.transform.eulerAngles = cam.transform.eulerAngles;
		rotX = oriRot.x;
		rotY = oriRot.y;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Touch touch in Input.touches) {
            if (touch.position.x > joystick.position.x + joystick.gameObject.GetComponent<RectTransform>().rect.width || touch.position.y > joystick.position.y + joystick.gameObject.GetComponent<RectTransform>().rect.width)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //Begin touch
                    initTouch = touch;  
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    //Swipe

                    float deltaX = initTouch.position.x - touch.position.x;
                    float deltaY = initTouch.position.y - touch.position.y;
                    rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
                    rotY -= deltaX * Time.deltaTime * rotSpeed * -dir;
                    rotX = Mathf.Clamp(rotX, -80f, 80f);
                    cam.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                    player.transform.eulerAngles = new Vector3(rotX, rotY, 0f);

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    initTouch = new Touch();
                }
            }
		}
	}
}
