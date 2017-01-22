using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rotateSpeed = 200f;
        transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
    }
}
