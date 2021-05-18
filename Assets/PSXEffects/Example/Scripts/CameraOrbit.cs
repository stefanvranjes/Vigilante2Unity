using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

	public Transform origin;
	public float speed = 20;

	public bool scrollControl = false;
	public float scrollSpeed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(origin);
		if(scrollControl)
			transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
		transform.RotateAround(origin.position, origin.transform.up, Time.deltaTime * speed);
	}
}
