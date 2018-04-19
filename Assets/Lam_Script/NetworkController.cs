using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : NetworkManager {

    public Transform mainCamera; // The main Camera
    public float cameraRotationRadius = 24f; // Radius for rotation of camera
    public float cameraRotationSpeed = 3f; // Speed rotation
    public bool isRotate = true; // want camera rotate or not

    // Position for camera
    public float xAxis = 0;
    public float yAxis = 0;
    public float zAxis = 0;

    float rotationAngle;

    

    public override void OnStartClient(NetworkClient client)
    {
        isRotate = false;
    }

    public override void OnStartHost()
    {
        isRotate = false;
    }

    public override void OnStopClient()
    {
        isRotate = true;
    }

    public override void OnStopHost()
    {
        isRotate = true;
    }

    // Update is called once per frame
    void Update() {

        // Want the main camera rotate or not
        if (!isRotate) {
            mainCamera.GetComponent<Camera>().enabled = false;
            return;
        }
        mainCamera.GetComponent<Camera>().enabled = true;
        // Calculate the rotation angle, and make sure it doesn't larger than 360 degrees
        rotationAngle += cameraRotationSpeed * Time.deltaTime;
        if(rotationAngle >= 360f) {
            rotationAngle -= 360f;
        }

        // Rotate the camera
        mainCamera.position = new Vector3(xAxis, yAxis, zAxis);
        mainCamera.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
        mainCamera.Translate(0f, cameraRotationRadius, -cameraRotationSpeed);
        mainCamera.LookAt(new Vector3(xAxis, yAxis, zAxis));
	}
}
