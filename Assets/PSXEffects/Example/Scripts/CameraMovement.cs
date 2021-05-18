using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	public float sensitivity = 10f;
	public Text infoText;
	public GameObject menu;

	private Camera cam;
	private bool menuEnabled = false;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		menuEnabled = false;
		menu.SetActive(menuEnabled);
	}
	
	// Update is called once per frame
	void Update () {
		infoText.text = "";
		if (Input.GetKeyDown(KeyCode.Q)) {
			menuEnabled = !menuEnabled;
			menu.SetActive(menuEnabled);
			Cursor.lockState = menuEnabled ? CursorLockMode.None : CursorLockMode.Locked;
			Cursor.visible = menuEnabled;
		}

		if (!menuEnabled) {
			transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
			transform.parent.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);

			RaycastHit hit;
			Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
			if (Physics.Raycast(ray, out hit)) {
				DemoInfo info = hit.transform.GetComponent<DemoInfo>();
				if (info) {
					infoText.text = "- " + info.info + " -";
				}
				if (Input.GetKeyDown(KeyCode.E)) {
					if (hit.transform.name == "DMOn")
						FindObjectOfType<PlayerMovement>().ToggleDemoMode(true);
					if (hit.transform.name == "DMOff")
						FindObjectOfType<PlayerMovement>().ToggleDemoMode(false);
				}
			}
		}
	}
}
