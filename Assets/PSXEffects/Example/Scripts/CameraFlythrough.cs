using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlythrough : MonoBehaviour {

	public float sensitivity = 2f;
	public float camSpeed = 10f;

	private float yaw;
	private float pitch;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Cursor.visible) {
			yaw += Input.GetAxis("Mouse X") * sensitivity;
			pitch -= Input.GetAxis("Mouse Y") * sensitivity;

			camSpeed += Input.GetAxisRaw("Mouse ScrollWheel") * 50;
		}

		transform.eulerAngles = new Vector3(pitch, yaw, 0);

		transform.Translate(camSpeed * Time.deltaTime * Vector3.forward * Input.GetAxis("Vertical"));
		transform.Translate(camSpeed * Time.deltaTime * Vector3.right * Input.GetAxis("Horizontal"));
	}

	public void LockCursor(bool lockIt) {
		if (lockIt) {
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.lockState = CursorLockMode.None;
		}

		Cursor.visible = !lockIt;
	}
}
