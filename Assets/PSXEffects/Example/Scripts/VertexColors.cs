using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VertexColors : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = mesh.vertices;

		Color[] colors = new Color[vertices.Length];

		for (int i = 0; i < vertices.Length; i++)
			colors[i] = Color.Lerp(Color.blue, Color.red, (float)i / vertices.Length);

		mesh.colors = colors;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
