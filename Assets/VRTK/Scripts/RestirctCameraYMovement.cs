using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestirctCameraYMovement : MonoBehaviour {
    GameObject cameraRig;
    public int minY = 0;
    public float maxY = 0.5f;
	// Use this for initialization
	void Start () {
        cameraRig = GameObject.FindWithTag("camera");   //Find the game object in the scene whose tag is "Ball".
    }
	
	// Update is called once per frame
	void Update () {
        // If Player's Y exceeds minY or maxY..
        if (cameraRig.transform.position.y <= minY || cameraRig.transform.position.y >= maxY)
        {
            // Create values between this range (minY to maxY) and store in yPos
            float yPos = Mathf.Clamp(cameraRig.transform.position.y, minY, maxY);

            // Assigns these values to the Transform.position component of the Player
            cameraRig.transform.position = new Vector3(cameraRig.transform.position.x, yPos, 0);
        }
    }
}
